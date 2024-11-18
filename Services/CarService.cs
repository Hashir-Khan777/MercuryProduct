using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /* The `CarService` constructor is initializing the `db` and `notificationService` fields of the
        `CarService` class with the values passed as arguments when an instance of `CarService` is created.
        This allows the `CarService` class to have access to the database context (`ApplicationDbContext`)
        and the notification service (`NotificationService`) that are needed for performing operations
        related to cars. */
        public CarService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>
        /// The AddCar function adds a car to the database and optionally adds a note with details if provided.
        /// </summary>
        /// <param name="CarModel">CarModel is a class representing a car entity with properties such as Id,
        /// Make, Model, Year, Color, etc. It is used to store information about a specific car that is being
        /// added to the database.</param>
        /// <param name="notes">The `notes` parameter in the `AddCar` method is a string that allows the user to
        /// provide additional notes or comments related to the car being added. These notes are then stored in
        /// the database along with the car information.</param>
        public void AddCar(CarModel car, string notes)
        {
            try
            {
                db.Cars.Add(car);
                db.SaveChanges();
                if (notes != null && notes.Trim() != "")
                {
                    db.Notes.Add(new NoteModel { note = notes, veh_id = car.Id, created_by_id = car.created_by_id, created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>
        /// The UpdateCar function updates a car entity in the database and handles any exceptions by notifying
        /// with an error message.
        /// </summary>
        /// <param name="CarModel">CarModel is a class representing a car entity with properties such as Id,
        /// Make, Model, Year, Color, etc. It is used to pass information about a car that needs to be updated
        /// in the database.</param>
        public void UpdateCar(CarModel car)
        {
            try
            {
                db.Cars.Update(car);
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
        /// The function `GetCarById` retrieves a `CarModel` object by its ID from a database, handling
        /// exceptions and returning null if an error occurs.
        /// </summary>
        /// <param name="id">The `id` parameter is an integer value that represents the unique identifier of the
        /// car you want to retrieve from the database.</param>
        /// <returns>
        /// The method `GetCarById` is returning a `CarModel` object or `null`.
        /// </returns>
        public CarModel? GetCarById(int id)
        {
            try
            {
                return db.Cars.Include(c => c.docs).Include(c => c.Company).FirstOrDefault(c => c.Id == id);
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
        /// The function `GetCars` retrieves a list of car models from a database, including related entities,
        /// and handles exceptions by returning null and notifying with an error message.
        /// </summary>
        /// <returns>
        /// A `List<CarModel>` is being returned by the `GetCars` method. If an exception occurs during the
        /// retrieval of cars from the database, a notification message is created and an error severity
        /// notification is sent using the `notificationService`. In this case, `null` is returned instead of
        /// the list of cars.
        /// </returns>
        public List<CarModel>? GetCars()
        {
            try
            {
                return db.Cars.OrderByDescending(c => c.created_at).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.docs).Include(c => c.Company).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CarModel>? GetCarsByManagerId(string ManagerId)
        {
            try
            {
                return db.Cars.Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.docs).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.Company.CompanyManagers.Any(cm => cm.manager_id == ManagerId)).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CarModel>? GetAllCarsByCompanyId(int company_id)
        {
            try
            {
                return db.Cars.Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.docs).Where(x => x.CompanyId == company_id).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CarModel>? GetCarsByCompanyId(int company_id)
        {
            try
            {
                return db.Cars.Where(x => x.deleted == false).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.docs).Where(x => x.CompanyId == company_id).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CarModel>? GetCarsByEmployeeId(string EmployeeId)
        {
            try
            {
                return db.Cars.Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.docs).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.Company.CompanyEmployees.Any(cm => cm.employee_id == EmployeeId)).OrderByDescending(c => c.created_at).ToList();
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
        /// The function `GetMakes` retrieves a list of distinct vehicle makes from a database table, handling
        /// any exceptions by notifying with an error message.
        /// </summary>
        /// <returns>
        /// A list of unique makes from the MasterVehicleTable in the database is being returned. If an
        /// exception occurs during the retrieval process, a notification message is created and the method
        /// returns null.
        /// </returns>
        public List<string>? GetMakes()
        {
            try
            {
                return db.MasterVehicleTable.Select(v => v.make).Distinct().ToList();
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
        /// The function `GetModelsByMake` retrieves a list of unique model names based on a given make from a
        /// database table, handling exceptions and notifying with an error message if needed.
        /// </summary>
        /// <param name="make">The `make` parameter in the `GetModelsByMake` method is a string that represents
        /// the make of a vehicle. The method retrieves a list of models associated with the specified make from
        /// the `MasterVehicleTable` in the database. If an exception occurs during the database query, an error
        /// notification</param>
        /// <returns>
        /// The method `GetModelsByMake` is returning a list of strings representing the models associated with
        /// a specific make of vehicles. If an exception occurs during the database query, it catches the
        /// exception, creates a notification message with the error details, notifies the user using the
        /// `notificationService`, and then returns `null`.
        /// </returns>
        public List<string>? GetModelsByMake(string? make)
        {
            try
            {
                return db.MasterVehicleTable.Where(v => v.make == make).Select(v => v.model).Distinct().ToList();
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
        /// The function `GetYear` retrieves a list of nullable integers representing years from a database
        /// table, handling exceptions by returning null and notifying with an error message.
        /// </summary>
        /// <returns>
        /// The method `GetYear()` is returning a `List<int?>` or a nullable list of integers. If the database
        /// query is successful, it will return a list of years from the `MasterYearTable`. If an exception
        /// occurs during the database query, it will catch the exception, create a notification message with
        /// the error details, notify the user using the `notificationService`, and return `null`.
        /// </returns>
        public List<int?>? GetYear()
        {
            try
            {
                return db.MasterYearTable.Select(y => y.Year).ToList();
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
        /// The function `GetCarsByDriverId` retrieves a list of car models based on a driver's ID and today's
        /// date, handling exceptions and returning null if an error occurs.
        /// </summary>
        /// <param name="driver_id">The `driver_id` parameter is used to filter and retrieve a list of
        /// `CarModel` objects associated with a specific driver. The method `GetCarsByDriverId` fetches cars
        /// from the database that match the provided `driver_id` and have a scheduled date equal to today's
        /// date.</param>
        /// <returns>
        /// The method `GetCarsByDriverId` is returning a list of `CarModel` objects that match the specified
        /// `driver_id` and have a `scheduled_date` equal to today's date. The list is ordered by the
        /// `scheduled_date` property. If an exception occurs during the retrieval process, an error
        /// notification message is created and sent using the `notificationService`, and `null` is
        /// </returns>
        public List<CarModel>? GetCarsByDriverId(string driver_id)
        {
            try
            {
                return db.Cars.OrderBy(c => c.scheduled_date).Where(c => c.driver_id == driver_id && c.scheduled_date.Date == DateTime.Today.Date).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.Company).ToList();
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
        /// The DeleteCar function removes a car from the database and handles any exceptions by notifying with
        /// an error message.
        /// </summary>
        /// <param name="CarModel">CarModel is a class representing a car entity in the application. It likely
        /// contains properties such as Id, Make, Model, Year, etc., to store information about a specific
        /// car.</param>
        public void DeleteCar(CarModel car)
        {
            try
            {
                car.deleted = true;
                db.Cars.Update(car);
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
