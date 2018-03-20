using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgLab
{
    public class SearchRequestModel
    {
        [Display(Name = "Имя файла")]
        public string FileName { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Дата от")]
        public DateTime? DateStart { get; set; }

        [Display(Name = "Дата до")]
        public DateTime? DateEnd { get; set; }

        [Display(Name = "Выдержка")]
        public string Exp { get; set; }


        public static IEnumerable<SelectListItem> _expList;
        public IEnumerable<SelectListItem> ExpList { get { return _expList; } set { } }

        [Display(Name = "Модель фотоаппарата")]
        public string PhotoModel { get; set; }

        public static IEnumerable<SelectListItem> _modelList;
        public IEnumerable<SelectListItem> ModelList { get { return _modelList; } set { } }

    }

    public class SettingsDataModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Путь")]
        public string Path { get; set; }


        [Display(Name = "Последнее обновление")]
        public DateTime LastUpdate { get; set; }

        [Display(Name = "Изображений найдено")]
        public long Count { get; set; }

    }

    public class ImageModel
    {
        [Key]
        public long Id { get; set; }
        public SettingsDataModel SourceSdm { get; set; }

        [Display(Name = "Имя файла")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Путь к файлу")]
        public string Path { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime DCreation { get; set; }
        [Display(Name = "Фокусное расстояние")]
        public string FocusDist { get; set; }
        [Display(Name = "Выдержка")]
        public string Exposure { get; set; }
        [Display(Name = "Модель фотоаппарата")]
        public string CameraModel { get; set; }
        [Display(Name = "Модель объектива")]
        public string LensModel { get; set; }
        [Display(Name = "Подспутниковая точка")]
        public string SubSatCoord { get; set; }
        [Display(Name = "Координаты центра")]
        public string CenterCoord { get; set; }
        [Display(Name = "Номер экспедиции")]
        public string ExpNum { get; set; }
        [Display(Name = "Номер радиограммы")]
        public string RadiogramNum { get; set; }

        [Display(Name = "MD5")]
        public string MD5Hash { get; set; }


    }

}