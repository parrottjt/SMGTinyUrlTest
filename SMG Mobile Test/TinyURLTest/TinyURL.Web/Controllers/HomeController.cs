using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TinyURL.Data.Services;
using TinyURL.Web.Models;

namespace TinyURL.Web.Controllers
{
    public class HomeController : Controller
    {
        IUploadedImage db;

        public HomeController(IUploadedImage uploadedImageDatabase)
        {
            db = uploadedImageDatabase;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new ImageViewModel();
            return View(model);
        }

        public ActionResult List()
        {
            var model = new ImageViewModel();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            string path = Server.MapPath("~/UploadedImages/");
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    file.SaveAs(path + file.FileName);
                }
            }

            return Json($"{files.Count} Image(s) have been Uploaded!");
        }
    }
}