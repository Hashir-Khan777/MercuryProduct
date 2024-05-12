using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace MecuryProduct.Services
{
    public class CarService
    {
        private readonly ApplicationDbContext db;

        public CarService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddCar(CarModel car, string notes)
        {
            db.Cars.Add(car);
            db.SaveChanges();
            if (notes != null && notes.Trim() != "")
            {
                db.Notes.Add(new NoteModel { note = notes, veh_id = car.Id, created_by_id = car.created_by_id, created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });
                db.SaveChanges();
            }
        }

        public void UpdateCar(CarModel car)
        {
            db.Cars.Update(car);
            db.SaveChanges();
        }

        public CarModel? GetCarById(int id)
        {
            return db.Cars.FirstOrDefault(c => c.Id == id);
        }

        public List<CarModel> GetCars()
        {
            return db.Cars.OrderByDescending(c => c.created_at).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).ToList();
        }

        public List<CarModel> GetCarsByDriverId(string driver_id)
        {
            return db.Cars.OrderBy(c => c.scheduled_date).Where(c => c.driver_id == driver_id && c.scheduled_date.Date == DateTime.Today.Date).Include(c => c.driver).Include(c => c.customer).Include(c => c.created_by).ToList();
        }

        public void DeleteCar(CarModel car)
        {
            db.Cars.Remove(car);
            db.SaveChanges();
        }
    }
}
