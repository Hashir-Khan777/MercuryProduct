using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class DocService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /* The `DocService` class in the provided C# code snippet has a constructor method `public
        DocService(ApplicationDbContext db, NotificationService notificationService)`. This constructor is
        used to initialize a new instance of the `DocService` class with the required dependencies. */
        public DocService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>
        /// The AddDoc function adds a document to the database and handles any exceptions by notifying the user
        /// with an error message.
        /// </summary>
        /// <param name="DocModel">DocModel is a model class representing a document in the application. It
        /// likely contains properties such as document title, content, author, creation date, etc. The AddDoc
        /// method is responsible for adding a new document to the database using an Entity Framework DbContext
        /// (db) and saving changes. If an exception</param>
        public void AddDoc(DocModel doc)
        {
            try
            {
                db.Docs.Add(doc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>
        /// The UpdateDoc function updates a document in a database and handles any exceptions by notifying with
        /// an error message.
        /// </summary>
        /// <param name="DocModel">DocModel is a model class that represents a document in the application. It
        /// likely contains properties such as document ID, title, content, author, creation date, etc. The
        /// UpdateDoc method is responsible for updating a document in the database using the provided DocModel
        /// object.</param>
        public void UpdateDoc(DocModel doc)
        {
            try
            {
                db.Docs.Update(doc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>
        /// The DeleteDoc function removes a document from the database and handles any exceptions by notifying
        /// with an error message.
        /// </summary>
        /// <param name="DocModel">DocModel is a model class that represents a document in the application. It
        /// likely contains properties such as document ID, title, content, author, creation date, etc. In the
        /// DeleteDoc method, an instance of DocModel is passed as a parameter to identify the document that
        /// needs to be deleted from</param>
        public void DeleteDoc(DocModel doc)
        {
            try
            {
                db.Docs.Remove(doc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>
        /// The function retrieves a list of documents based on a vehicle ID, handling exceptions and notifying
        /// errors if they occur.
        /// </summary>
        /// <param name="veh_id">The `veh_id` parameter is an integer representing the ID of a vehicle.</param>
        /// <returns>
        /// The method `GetDocsByVehId` is returning a list of `DocModel` objects that match the provided
        /// `veh_id`. If an exception occurs during the retrieval process, an error notification message is
        /// created and sent using the `notificationService`, and `null` is returned.
        /// </returns>
        public List<DocModel>? GetDocsByVehId(int veh_id)
        {
            try
            {
                return db.Docs.Where(d => d.veh_id == veh_id).Include(d => d.note).ToList();
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
