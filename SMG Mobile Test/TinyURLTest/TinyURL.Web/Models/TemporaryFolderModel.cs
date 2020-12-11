using System;
using System.Collections.Generic;
using System.IO;

namespace TinyURL.Web.Models
{
    public class TemporaryFolderModel
    {
        public List<string> FileNames = new List<string>();
        public string TemporaryFolderPath;

        public TemporaryFolderModel()
        {
            TemporaryFolderPath = Path.Combine(@"~\UploadedImages\Temporary\", Guid.NewGuid().ToString() + @"\");
        }

        public void DeleteTemporaryFolder()
        {
            DeleteTemporaryFilesInFolder(TemporaryFolderPath);
            Directory.Delete(TemporaryFolderPath);
        }

        public void DeleteTemporaryFilesInFolder(string path)
        {
            var tempFiles = Directory.GetFiles(path);
            foreach (var tempFile in tempFiles)
            {
                DeleteTemporaryFile(tempFile);
            }
        }

        public void DeleteTemporaryFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}