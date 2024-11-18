using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class StateFormService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /// <summary>Initializes a new instance of the StateFormService class.</summary>
        /// <param name="db">The application's database context.</param>
        /// <param name="notificationService">The notification service used for sending notifications.</param>
        public StateFormService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>Adds a StateFormModel to the database.</summary>
        /// <param name="state_form">The StateFormModel to be added.</param>
        /// <exception cref="Exception">Thrown when an error occurs while adding the StateFormModel to the database.</exception>
        public void Add(StateFormModel state_form)
        {
            try
            {
                db.StateForm.Add(state_form);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>Retrieves a list of StateFormModel objects from the database.</summary>
        /// <returns>A list of StateFormModel objects with associated documents, or null if an exception occurs.</returns>
        public List<StateFormModel>? Get()
        {
            try
            {
                return db.StateForm.Include(s => s.doc).Include(s => s.Company).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<StateFormModel>? GetByManagerId(string ManagerId)
        {
            try
            {
                return db.StateForm.Include(s => s.doc).Include(s => s.Company).ThenInclude(c => c.CompanyManagers).Where(s => s.Company.CompanyManagers.Any(x => x.manager_id == ManagerId)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<StateFormModel>? GetAllByCompanyId(int company_id)
        {
            try
            {
                return db.StateForm.Include(s => s.doc).Include(s => s.Company).ThenInclude(c => c.CompanyManagers).Where(s => s.CompanyId == company_id).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<StateFormModel>? GetByCompanyId(int company_id)
        {
            try
            {
                return db.StateForm.Include(s => s.doc).Include(s => s.Company).ThenInclude(c => c.CompanyManagers).Where(s => s.CompanyId == company_id && !s.deleted).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<StateFormModel>? GetByEmployeeId(string EmployeeId)
        {
            try
            {
                return db.StateForm.Include(s => s.doc).Include(s => s.Company).ThenInclude(c => c.CompanyManagers).Where(s => s.Company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<StateFormModel>? GetByCompanyId(int? CompanyId)
        {
            try
            {
                return db.StateForm.Where(s => s.CompanyId == CompanyId).Include(s => s.doc).Include(s => s.Company).ToList();
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
        /// Retrieves a StateFormModel by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the StateFormModel to retrieve.</param>
        /// <returns>The StateFormModel with the specified ID if found; otherwise, null.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the retrieval process.</exception>
        public StateFormModel? GetById(int id)
        {
            try
            {
                return db.StateForm.FirstOrDefault(c => c.Id == id);
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
        /// Deletes a StateFormModel from the database.
        /// </summary>
        /// <param name="state_form">The StateFormModel to be deleted.</param>
        /// <remarks>
        /// This method removes the specified StateFormModel from the database. If an exception occurs during the deletion process,
        /// an error notification message is created and displayed using the notification service.
        /// </remarks>
        public void Delete(StateFormModel state_form)
        {
            try
            {
                state_form.deleted = true;
                db.StateForm.Update(state_form);
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
        /// Updates the state form model in the database.
        /// </summary>
        /// <param name="state_form">The StateFormModel object to be updated.</param>
        /// <exception cref="Exception">Thrown when an error occurs during the update process.</exception>
        public void Update(StateFormModel state_form)
        {
            try
            {
                db.StateForm.Update(state_form);
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
