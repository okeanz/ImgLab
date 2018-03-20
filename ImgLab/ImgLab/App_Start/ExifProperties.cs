using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExifLibrary;

namespace ImgLab
{
    public static class ExifProperties
    {
        public static bool UpdateExifTagsNew(ImageModel mdl)
        {
            ExifFile ef = null;
            try
            {
                ef = ExifFile.Read(mdl.Path);
            }
            catch (Exception)
            {
                return false;
            }

            if (ef != null)
            {
                try
                {
                    ef.Properties[ExifTag.ImageDescription] = new ExifAscii(ExifTag.ImageDescription, mdl.Description);

                    GPSLatitudeLongitude Latlocation = new GPSLatitudeLongitude(ExifTag.GPSLatitude, new MathEx.UFraction32[3]);
                    //var dg = Latlocation.Degrees;
                    //dg.Set(15, 5);
                    //Latlocation.Degrees = dg;
                    GPSLatitudeLongitude Longlocation = new GPSLatitudeLongitude(ExifTag.GPSLongitude, new MathEx.UFraction32[3]);

                    ef.Properties[ExifTag.GPSLatitude] = Latlocation;
                    ef.Properties[ExifTag.GPSLongitude] = Longlocation;

                    ef.Save(mdl.Path.Insert(mdl.Path.IndexOf('.'), "new"));
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            else
                return false;
            return true;
        }
    }
}