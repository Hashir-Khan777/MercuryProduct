using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;

namespace MecuryProduct.Services
{
    public class DocService
    {
        private readonly ApplicationDbContext db;

        public DocService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddDoc(DocModel doc)
        {
            db.Docs.Add(doc);
            db.SaveChanges();
        }

        public void UpdateDoc(DocModel doc)
        {
            db.Docs.Update(doc);
            db.SaveChanges();
        }

        public void DeleteDoc(DocModel doc)
        {
            db.Docs.Remove(doc);
            db.SaveChanges();
        }

        public List<DocModel> GetDocsByVehId(int veh_id)
        {
            return db.Docs.Where(d => d.veh_id == veh_id).Include(d => d.note).ToList();
        }
    }
}
