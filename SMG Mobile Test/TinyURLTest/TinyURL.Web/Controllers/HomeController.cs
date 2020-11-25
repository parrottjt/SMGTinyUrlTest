using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyURL.Data.Services;

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
            var model = db;
            return View(model);
        }

        public ActionResult Images()
        {
            var model = db.GetAll();
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}