using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Linq.Dynamic.Core;

namespace MecuryProduct.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public CarService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

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
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void UpdateCar(CarModel car)
        {
            try
            {
                db.Cars.Update(car);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public CarModel? GetCarById(int id)
        {
            try
            {
                return db.Cars.Include(c => c.docs).FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CarModel>? GetCars()
        {
            try
            {
                return db.Cars.OrderByDescending(c => c.created_at).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).Include(c => c.docs).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<string>? GetMakes()
        {
            try
            {
                return db.MasterVehicleTable.Select(v => v.make).Distinct().ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<string>? GetModelsByMake(string? make)
        {
            try
            {
                return db.MasterVehicleTable.Where(v => v.make == make).Select(v => v.model).Distinct().ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<int?>? GetYear()
        {
            try
            {
                return db.MasterYearTable.Select(y => y.Year).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CarModel>? GetCarsByDriverId(string driver_id)
        {
            try
            {
                return db.Cars.OrderBy(c => c.scheduled_date).Where(c => c.driver_id == driver_id && c.scheduled_date.Date == DateTime.Today.Date).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void DeleteCar(CarModel car)
        {
            try
            {
                db.Cars.Remove(car);
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
