using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace ImgLab
{
    public static class ImageDB
    {
        static List<ImageModel> DB;

        public static void Load()
        {
            DB = new List<ImageModel>();
            using (ILDBContext db = new ILDBContext())
            {
                foreach (var img in db.Images)
                    DB.Add(img);

                var exps = db.Images.Select(x => x.Exposure).Distinct();
                var models = db.Images.Select(x => x.CameraModel).Distinct();


                var explist = exps.Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
                explist.RemoveAll(x => x.Text == null || x.Text == "");
                explist.Add(new SelectListItem() { Text = "Отсутствует", Value = "", Selected = true });
                SearchRequestModel._expList = explist;

                var modellist = models.Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
                modellist.RemoveAll(x => x.Text == null || x.Text == "");
                modellist.Add(new SelectListItem() { Text = "Отсутствует", Value = "", Selected = true });
                SearchRequestModel._modelList = modellist;
            }
        }


        public static string[] GetFilteredNames(string filter)
        {
            return DB.Select(x => x.Name).Where(x => x.Contains(filter)).ToArray();
        }

        public static string[] GetNames()
        {
            return DB.Select(x => x.Name).ToArray();
        }

        public static string GetPath(string Id)
        {
            return DB.Where(x => x.Id == long.Parse(Id)).First().Path;
        }

        public static string[] GetDates()
        {
            return DB.Select(x => x.DCreation.ToString()).ToArray();
        }

        public static ImageModel GetInfo(string Id)
        {
            return DB.Where(x => x.Id == long.Parse(Id)).First();
        }

    }
}