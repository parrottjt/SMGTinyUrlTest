using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Helpers;
using TinyURL.Data.Models;

namespace TinyURL.Web.Models
{
    public class ImageViewModel
    {
        public List<string> FileNames { get; set; }

        public string CreateTinyUrl(string url)
        {
            try
            {
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + "smgtinyurltest.azurewebsites.net/" + url);
                var response = request.GetResponse();

                string tinyUrl;

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    tinyUrl = reader.ReadToEnd();
                }

                return tinyUrl;
            }
            catch (Exception exception)
            {

                return exception.Message;
            }
        }

        public void ValidateFile(HttpPostedFileBase file, string uploadPath)
        {
            string newFileName;
            bool fileExists;
            try
            {
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
                string fileExtension = file.FileName.Split('.')[1];

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
                        file.SaveAs(uploadPath + file.FileName);
                    }
                }
            }
            catch (Exception exceptionType)
            {

                //validationMessage = $"File Couldn't be processed with exception: {exceptionType.Message}";
            }
        }
    }
}