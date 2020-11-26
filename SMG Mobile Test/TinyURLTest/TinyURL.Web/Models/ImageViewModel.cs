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

        public void ValidateFile(WebImage webImage, out string path, out bool fileExists, out string validationMessage)
        {
            string newFileName = "";
            try
            {
                string fileExtension = webImage.FileName.Split('.')[1];
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
                    validationStatus = $"Valid webImage types are HEIC/HEIF, WEBP, PNG, JPEG, SVG, PDF, JPG & GIF\n" +
                                       $"File uploaded had type {fileExtension}";
                    fileExists = true;
                }
                else
                {
                    if (webImage.GetBytes().Length > 10000000)
                    {
                        validationStatus = "Upload size limited to 10MB";
                        fileExists = true;
                    }
                    else
                    {
                        newFileName = Path.GetFileName(webImage.FileName);
                        path = @"UploadedImages\" + newFileName;


                        try
                        {
                            HttpWebRequest.Create("smgtinyurltest.azurewebsites.net/" + path).GetResponse();
                            fileExists = true;

                            validationStatus = "File has already been uploaded to server";
                        }
                        catch
                        {
                            fileExists = false;

                            validationStatus = "File uploaded successful";
                        }

                        if (!fileExists)
                        {
                            webImage.Save(@"~\" + path);
                        }
                    }
                }

                validationMessage = validationStatus;
            }
            catch (Exception exceptionType)
            {
                path = "";
                validationMessage = $"File Couldn't be processed with exception: {exceptionType.Message}";
                fileExists = true;
            }

        }
    }
}