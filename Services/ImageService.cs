using MecuryProduct.Data;

namespace MecuryProduct.Services
{
    public class ImageService
    {
        private readonly ApplicationDbContext db;

        public ImageService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddImage(ImageModel image)
        {
            db.Images.Add(image);
            db.SaveChanges();
        }
    }
}
