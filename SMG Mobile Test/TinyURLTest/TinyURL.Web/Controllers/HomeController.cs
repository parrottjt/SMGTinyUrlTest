using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyURL.Data.Services;
using TinyURL.Web.Models;

namespace TinyURL.Web.Controllers
{
    public class HomeController : Controller
    {
        IUploadedImage db;

        string UploadedImagePath = @"~\UploadedImages\";
        public static string username = "";

        ImageViewModel imageViewModel;

        public HomeController(IUploadedImage db)
        {
            this.db = db;
            imageViewModel = new ImageViewModel();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var model = db.GetAll();
            return View(model);
        }

        public ActionResult UserUploadedList()
        {
            var model = db.GetAll().Where(image => image.NameOfUploader == username);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadToMainFolder()
        {
            var path = Server.MapPath(UploadedImagePath);
            List<string> fileNames = new List<string>();
            try
            {
                username = $"{Guid.NewGuid()}";
                HttpFileCollectionBase files = Request.Files;
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        if (!System.IO.File.Exists(Path.Combine(path, files[i].FileName)))
                        {
                            HttpPostedFileBase file = files[i];

                            var image = new Data.Models.UploadedImage { Id = db.GetAll().Count() ,FileName = file.FileName, TinyURL = imageViewModel.CreateTinyUrl(@"UploadedImages\" + file.FileName), NameOfUploader = username};
                            db.AddUploadedImage(image);

                            imageViewModel.ValidateFile(file, path);
                            fileNames.Add(file.FileName);
                        }
                    }
                }

                

                imageViewModel.FileNames = fileNames;

                return Json($"{files.Count} Image(s) have been Uploaded!");
            }
            catch (Exception exception)
            {

                return Json(exception.Message + " *** " + exception.Data);
            }
        }
    }
}