using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TinyURL.Web.Models;

namespace TinyURL.Web
{
    public class FileUpload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //var model = new ImageViewModel();
            //context.Response.Write("Processing Request");
            //string validationMessage = "";
            //HttpFileCollection files = context.Request.Files;
            //if (context.Request.Files.Count > 0)
            //{
            //    if (files.Count > 0)
            //    {
            //        for (int i = 0; i < files.Count; i++)
            //        {
            //            HttpPostedFileBase file = files[i];
            //            model.ValidateFile(file, "");
            //        }
            //    }
            //}
            //context.Response.ContentType = "text/plain";
            //context.Response.Write(validationMessage);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}