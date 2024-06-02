using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class StateFormService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public StateFormService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

        public void Add(StateFormModel state_form)
        {
            try
            {
                db.StateForm.Add(state_form);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<StateFormModel>? Get()
        {
            try
            {
                return db.StateForm.Include(s => s.doc).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public StateFormModel? GetById(int id)
        {
            try
            {
                return db.StateForm.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void Delete(StateFormModel state_form)
        {
            try
            {
                db.StateForm.Remove(state_form);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void Update(StateFormModel state_form)
        {
            try
            {
                db.StateForm.Update(state_form);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }
    }
}
