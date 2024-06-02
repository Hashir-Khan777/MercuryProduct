using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class DocService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public DocService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

        public void AddDoc(DocModel doc)
        {
            try
            {
                db.Docs.Add(doc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void UpdateDoc(DocModel doc)
        {
            try
            {
                db.Docs.Update(doc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void DeleteDoc(DocModel doc)
        {
            try
            {
                db.Docs.Remove(doc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<DocModel>? GetDocsByVehId(int veh_id)
        {
            try
            {
                return db.Docs.Where(d => d.veh_id == veh_id).Include(d => d.note).ToList();
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
