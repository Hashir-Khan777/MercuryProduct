using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class NoteService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /// <summary>Initializes a new instance of the NoteService class.</summary>
        /// <param name="db">The application's database context.</param>
        /// <param name="notificationService">The notification service used for sending notifications.</param>
        public NoteService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>
        /// Retrieves a list of notes associated with a specific vehicle ID.
        /// </summary>
        /// <param name="VehId">The ID of the vehicle to retrieve notes for.</param>
        /// <returns>A list of NoteModel objects related to the specified vehicle ID, including the creator information.</returns>
        /// <remarks>If an exception occurs during the retrieval process, an error notification is sent and null is returned.</remarks>
        public List<NoteModel>? GetNotesByVehicleId(int VehId)
        {
            try
            {
                return db.Notes.Where(n => n.veh_id == VehId).Include(n => n.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a list of notes based on the state form ID.
        /// </summary>
        /// <param name="sf_id">The state form ID to filter notes by.</param>
        /// <returns>A list of NoteModel objects related to the specified state form ID.</returns>
        /// <remarks>If an exception occurs during the retrieval process, a notification message is sent and null is returned.</remarks>
        public List<NoteModel>? GetNotesByStateFormId(int? sf_id)
        {
            try
            {
                return db.Notes.Where(n => n.sf_id == sf_id).Include(n => n.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>Adds a new note to the database.</summary>
        /// <param name="note">The note model to be added.</param>
        /// <remarks>
        /// This method adds the provided note model to the database and saves the changes.
        /// If an exception occurs during the process, an error notification message is created and displayed.
        /// </remarks>
        public void AddNote(NoteModel note)
        {
            try
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }
    }
}
