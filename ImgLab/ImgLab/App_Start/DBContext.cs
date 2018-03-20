using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.IO;
using ExifLibrary;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;
namespace ImgLab
{
    public class ILDBContext : DbContext
    {
#if DEBUGHOME
        public ILDBContext():base("ILDBv13")
#else
        public ILDBContext()
            : base("ILDB")
#endif
        {

        }

        public DbSet<ImageModel> Images { get; set; }
        public DbSet<SettingsDataModel> Settings { get; set; }
    }

    public static class DBControl
    {
        static MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider();

        public static void LoadNew(SettingsDataModel sdm, bool inclusive = false)
        {
            string[] paths;
            if(Path.GetExtension(sdm.Path) == ".txt")
            {
                var file = File.OpenText(sdm.Path);
                var arr = new List<string[]>();

                while (!file.EndOfStream)
                {
                    var str = file.ReadLine();
                    arr.Add(str.Split(';'));
                }
            }
            if (inclusive)
                paths = Directory.EnumerateFiles(sdm.Path, ".", SearchOption.AllDirectories).Select(x=>x.ToLower()).ToArray();
            else
                paths = Directory.GetFiles(sdm.Path).Select(x => x.ToLower()).ToArray();
            paths = paths.Where(x => x.EndsWith(".png") || x.EndsWith(".jpg")).ToArray();

            using (ILDBContext db = new ILDBContext())
            {
                sdm.LastUpdate = DateTime.Now;
                sdm.Count = paths.Length;
                db.Settings.Add(sdm);

                foreach (var p in paths)
                {
                    AddNew(db,sdm, p);

                }
                db.SaveChanges();
            }
            ImageDB.Load();
        }

        static void AddNew(ILDBContext db, SettingsDataModel sdm, string path)
        {
            var imodel = new ImageModel()
            {
                Name = Path.GetFileName(path),
                Path = path,
                DCreation = DateTime.Now,
                SourceSdm = sdm
            };

            var ms = new MemoryStream();
            Bitmap bmp = null;
            try
            {
                var img = Image.FromFile(path);
                bmp = new Bitmap(img);

                bmp.Save(ms, ImageFormat.Jpeg);
                var buff = csp.ComputeHash(ms.ToArray());
                imodel.MD5Hash = Convert.ToBase64String(buff);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write(e.ToString());
            }
            


            ExifFile f = null;
            try
            {
                f = ExifFile.Read(path);
            }
            catch (Exception) { }

            if (f != null)
            {

                imodel.DCreation = f.Properties.ContainsKey(ExifTag.DateTimeOriginal) ? (DateTime.Parse(f.Properties[ExifTag.DateTimeOriginal].Value.ToString())) : DateTime.Now;
                imodel.FocusDist = f.Properties.ContainsKey(ExifTag.FocalLength) ? f.Properties[ExifTag.FocalLength].Value.ToString() : "";
                imodel.Exposure = f.Properties.ContainsKey(ExifTag.ExposureTime) ? f.Properties[ExifTag.ExposureTime].Value.ToString() : "";
                imodel.LensModel = "";
                imodel.CameraModel = f.Properties.ContainsKey(ExifTag.Model) ? f.Properties[ExifTag.Model].Value.ToString() : "";
            }

            db.Images.Add(imodel);
        }

        public static void Update(SettingsDataModel sdm)
        {
            using (var db = new ILDBContext())
            {
                string[] paths = Directory.GetFiles(sdm.Path).Select(x => x.ToLower()).ToArray();
                paths = paths.Where(x => x.EndsWith(".png") || x.EndsWith(".jpg")).ToArray();

                foreach (var p in paths)
                {
                    string hash = "";
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var img = new Bitmap(Image.FromFile(p));
                        img.Save(ms, ImageFormat.Jpeg);
                        var buff = csp.ComputeHash(ms.ToArray());
                        hash = Convert.ToBase64String(buff);
                    }
                    var lst = db.Images.ToList();
                    var thesamemd5 = lst.Where(x => x.MD5Hash == hash).ToArray();
                    if(thesamemd5 == null || thesamemd5.Count() == 0)
                    {
                        AddNew(db, sdm, p);
                        sdm.Count++;
                    }

                }
                sdm.LastUpdate = DateTime.Now;

                db.Entry(sdm).State = EntityState.Modified;
                
                db.SaveChanges();
            }

            ImageDB.Load();

        }

        public static void RemoveSetting(SettingsDataModel sdm)
        {
            using (var db = new ILDBContext())
            {
                db.Entry(sdm).State = EntityState.Deleted;
                db.SaveChanges();
                ImageDB.Load();
            }
        }
    }

}