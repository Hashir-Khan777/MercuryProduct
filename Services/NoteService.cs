using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class NoteService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public NoteService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

        public List<NoteModel>? GetNotesByVehicleId(int VehId)
        {
            try
            {
                return db.Notes.Where(n => n.veh_id == VehId).Include(n => n.created_by).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<NoteModel>? GetNotesByStateFormId(int? sf_id)
        {
            try
            {
                return db.Notes.Where(n => n.sf_id == sf_id).Include(n => n.created_by).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void AddNote(NoteModel note)
        {
            try
            {
                db.Notes.Add(note);
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
