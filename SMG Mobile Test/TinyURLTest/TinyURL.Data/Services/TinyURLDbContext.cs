using System.Data.Entity;
using TinyURL.Data.Models;

namespace TinyURL.Data.Services
{
    public class TinyURLDbContext : DbContext
    {
        public TinyURLDbContext() : base("SMGTestDataBaseEntities")
        {

        }
        public DbSet<UploadedImage> UploadedImages { get; set; }
    }
}