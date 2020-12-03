using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TinyURL.Web.Models;

namespace TinyURL.Web
{
    public class FileUpload : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string validationMessage = "";
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                foreach (string file in files)
                {
                    try
                    {
                        var model = new ImageViewModel();
                        model.ValidateFile(files[file], out string path, out bool fileExists, out validationMessage);
                    }
                    catch
                    {
                    }
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(validationMessage);
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