using MecuryProduct.Data;
using Radzen;

namespace MecuryProduct.Services
{
    public class ProductionService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public ProductionService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

        public List<string>? GetAllSections()
        {
            try
            {
                return db.MasterProductionTable.Select(p => p.section).Distinct().ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<string>? GetRowsBySection(string section)
        {
            try
            {
                return db.MasterProductionTable.Where(p => p.section == section).Select(p => p.row).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }
    }
}
