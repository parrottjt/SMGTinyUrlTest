using System.Collections.Generic;
using System.Linq;
using TinyURL.Data.Models;

namespace TinyURL.Data.Services
{
    public class InMemoryUploadedImages : IUploadedImage
    {
        List<UploadedImage> db = new List<UploadedImage>();

        public IEnumerable<UploadedImage> GetAll()
        {
            return db.OrderBy(image => image.Id);
        }

        public bool IsConnectionValid()
        {
            return false;
        }

        public string ReturnConnectionServer()
        {
            throw new System.NotImplementedException();
        }

        public UploadedImage Get(int id)
        {
            return db.FirstOrDefault(image => image.Id == id);
        }

        //This is the start for the web scraper
        public UploadedImage Get(string fileName)
        {
            return db.FirstOrDefault(image => image.TinyURL == fileName);
        }

        public void AddUploadedImage(UploadedImage uploadedImages)
        {
            uploadedImages.Id = db.Count;
            db.Add(uploadedImages);
        }

        public void UpdateUploadedImage(UploadedImage uploadedImage)
        {
            db[db.FindIndex(image => image == uploadedImage)] = uploadedImage;
        }

        public void DeleteUploadedImage(UploadedImage uploadedImages)
        {
            db.Remove(uploadedImages);
        }
    }
}