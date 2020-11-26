using System;
using System.Collections.Generic;
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

        public HomeController(IUploadedImage uploadedImageDatabase)
        {
            db = uploadedImageDatabase;
        }

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
    }
}