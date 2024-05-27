using MecuryProduct.Data;

namespace MecuryProduct.Services
{
    public class ProductionService
    {
        private readonly ApplicationDbContext db;

        public ProductionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<string> GetAllSections()
        {
            return db.MasterProductionTable.Select(p => p.section).Distinct().ToList();
        }

        public List<string> GetRowsBySection(string section)
        {
            return db.MasterProductionTable.Where(p => p.section == section).Select(p => p.row).ToList();
        }
    }
}
