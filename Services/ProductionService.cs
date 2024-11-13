using MecuryProduct.Data;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class ProductionService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /// <summary>Initializes a new instance of the ProductionService class.</summary>
        /// <param name="db">The application database context.</param>
        /// <param name="notificationService">The notification service.</param>
        public ProductionService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>Retrieves a list of all unique sections from the Master Production Table.</summary>
        /// <returns>A list of strings representing all unique sections, or null if an exception occurs.</returns>
        public List<string>? GetAllSections()
        {
            try
            {
                return db.MasterProductionTable.Select(p => p.section).Distinct().ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>Retrieves rows from the Master Production Table based on a specified section.</summary>
        /// <param name="section">The section to filter by.</param>
        /// <returns>A list of strings representing rows from the Master Production Table for the specified section, or null if an exception occurs.</returns>
        public List<string>? GetRowsBySection(string section)
        {
            try
            {
                return db.MasterProductionTable.Where(p => p.section == section).Select(p => p.row).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }
    }
}
