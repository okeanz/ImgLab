using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Net;
using ExifLibrary;

namespace ImgLab.Controllers
{
    public class HomeController : Controller
    {
        //Количество выводимых картинок на одной странице поиска
        private static int SHOWCOUNT = 6;

        private ILDBContext db = new ILDBContext();
        // GET: Index
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.number = db.Images.Count();
            return View();
        }

        // GET: Database
        [HttpGet]
        public ActionResult Database()
        {
            SearchRequestModel._expList.Where(x => x.Selected == true).First().Selected = false;
            SearchRequestModel._expList.Where(x => x.Text == "Отсутствует").First().Selected = true;
            SearchRequestModel._modelList.Where(x => x.Selected == true).First().Selected = false;
            SearchRequestModel._modelList.Where(x => x.Text == "Отсутствует").First().Selected = true;


            SearchRequestModel srm = new SearchRequestModel();

            Random r = new Random();
            var randlist = db.Images.AsEnumerable().OrderBy(x => r.Next()).Take(6).OrderBy(x=>x.DCreation).ToList();
            

            ViewBag.Pages = MakePages(randlist,SHOWCOUNT);


            return View(srm);
        }


        // POST: Database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Database(SearchRequestModel mdl)
        {
            if (ModelState.IsValid)
            {
                var list = db.Images.AsEnumerable().Where
                    (
                       x => mdl.FileName != null && x.Name != null ? x.Name.Contains(mdl.FileName.ToLower()) : false
                    || mdl.Description != null && x.Description != null ? x.Description.Contains(mdl.Description.ToLower()) : false
                    || mdl.DateStart != null && x.DCreation != null ? (x.DCreation >= mdl.DateStart) : false
                    || mdl.DateEnd != null && x.DCreation != null ? (x.DCreation < mdl.DateEnd) : false
                    || mdl.Exp != null && x.Exposure != null ? x.Exposure == mdl.Exp : false
                    || mdl.PhotoModel != null && x.CameraModel != null ? x.CameraModel == mdl.PhotoModel : false
                    );
                ViewBag.Pages = MakePages(list, SHOWCOUNT);

                

                return View(mdl);
            }
            return View(mdl);
        }

        List<List<dynamic>> MakePages(IEnumerable<dynamic> list, int countperpage)
        {
            var pages = new List<List<dynamic>>();

            for (int i = 0; i < Math.Ceiling((double)list.Count() / countperpage); i++)
            {
                var lst = list.Where((x, y) => y >= i * countperpage && y < (i * countperpage + countperpage)).ToList<dynamic>();
                pages.Add(lst);
            }
            return pages;
        }


        [HttpGet]
        public async Task<ActionResult> ImageEdit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ImageModel imdl = await db.Images.FindAsync(id);

            if (imdl == null)
                return HttpNotFound();

            return PartialView("_ImageEdit",imdl);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ImageEdit([Bind(Include="Id,Name,Description,Path,DCreation,FocusDist,Exposure,CameraModel,LensModel,SubSatCoord,CenterCoord,ExpNum,RadiogramNum")]ImageModel mdl)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    db.Entry(mdl).State = System.Data.Entity.EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return HttpNotFound("Cant update DB");
                }

                //if (!ExifProperties.UpdateExifTagsNew(mdl))
                //    return HttpNotFound("exif error");


                return PartialView("_ModalSuccess");
            }
            return PartialView("_ImageEdit", mdl);
        }



        // GET: GetImage
        [HttpGet]
        public FilePathResult GetImage(string Id)
        {
            return File(ImageDB.GetPath(Id), "image/png");
        }

        
        //GET: GetImageThumb
        [HttpGet]
        public FileContentResult GetImageThumb(string Id)
        {

            Image img = null;
            try
            {
                img = (Image)new Bitmap(ImageDB.GetPath(Id));
            }
            catch(Exception) { }

            if (img != null)
                using (MemoryStream ms = new MemoryStream())
                {
                    img = (Image)new Bitmap(img,new Size(300,200));
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return File(ms.ToArray(), "image/png");
                }
            else
                return null;

        }

        [HttpGet]
        public JsonResult GetInfo(string Id)
        {
            var mdl = ImageDB.GetInfo(Id);
            return Json(mdl);
        }


        // GET: Settings
        [HttpGet]
        public ActionResult Settings()
        {
            return View();
        }
    }
}