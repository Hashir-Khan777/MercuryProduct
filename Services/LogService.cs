using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public LogService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public List<AuditLogModel>? GetLogs()
        {
            try
            {
                return db.Logs.Include("user").ToList();
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
