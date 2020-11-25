using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TinyURL.Data.Models;
using TinyURL.Data.Services;

namespace TinyURL.Web.Controllers
{
    //This was the old way that I was using to get the image to function properly

    public class ImageController : Controller
    {
        readonly IUploadedImage db;

        string errorMessage;

        public ImageController(IUploadedImage uploadedImageDatabase)
        {
            db = uploadedImageDatabase;
        }

        // GET: UploadImage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var model = db.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                ValidateFile(file, out var path);
                var image = new UploadedImage { FileName = file.FileName, TinyURL = CreateTinyUrl(path) };
                try
                {
                    //db.AddUploadedImage(image);
                    return View(image);
                }
                catch (Exception exceptionType)
                {
                    ViewBag.Error = exceptionType.Message;
                    return View(image);
                }
            }

            return View("Index");
        }

        /// <summary>
        /// 
        /// ~Functionality~
        /// This contains the validation for the file being uploaded.
        /// 
        /// </summary>
        /// <param name="file"></param>
        void ValidateFile(HttpPostedFileBase file, out string path)
        {
            try
            {
                string fileExtension = file.FileName.Split('.')[1];
                List<string> validExtensions = new List<string>
                {
                    "heic",
                    "heif",
                    "webp",
                    "png",
                    "jpeg",
                    "svg",
                    "pdf",
                    "jpg",
                    "gif"
                };

                string validationStatus;

                path = "Not Valid";

                if (!validExtensions.Contains(fileExtension.ToLower()))
                {
                    validationStatus = $"Valid file types are HEIC/HEIF, WEBP, PNG, JPEG, SVG, PDF, JPG & GIF\n" +
                                       $"File uploaded had type {fileExtension}";
                }
                else
                {
                    if (file.ContentLength > 10000000)
                    {
                        validationStatus = "Upload size limited to 10MB";
                    }
                    else
                    {
                        validationStatus = "File uploaded successful";
                        path = Server.MapPath($"~/{file.FileName}");
                        Image img = Image.FromFile(file.FileName);
                        img.Save(path);
                    }
                }

                ViewBag.ValidationStatus = validationStatus;
            }
            catch (Exception exceptionType)
            {
                path = "";
                ViewBag.ValidationStatus = $"File Couldn't be processed with exception: {exceptionType.Message}";
                RedirectToAction("Index");
            }

        }

        /// <summary>
        /// 
        /// Currently this isn't working as hoped. I have a feeling it is due to finding the path to the image in the folder structure.
        ///
        /// ~Functionality~
        /// This accesses the tinyurl web api to create a tinyurl link to the image.
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        string CreateTinyUrl(string url)
        {
            try
            {
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + (url));
                var response = request.GetResponse();

                string tinyUrl;

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    tinyUrl = reader.ReadToEnd();
                }

                return tinyUrl;
            }
            catch (Exception)
            {

                return url;
            }
        }
    }
}