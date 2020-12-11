using System.Collections.Generic;
using TinyURL.Data.Models;

namespace TinyURL.Data.Services
{
    public interface IUploadedImage
    {
        IEnumerable<UploadedImage> GetAll();
        UploadedImage Get(int id);
        UploadedImage Get(string fileName);
        void AddUploadedImage(UploadedImage uploadedImages);
    }
}