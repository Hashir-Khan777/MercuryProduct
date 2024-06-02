using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public CustomerService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

        public void AddCustomer(CustomerModel customer)
        {
            try
            {
                customer.search = $"{customer.cfname} {customer.clname} {customer.cphone_number}";
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<CustomerModel>? GetCustomers()
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public CustomerModel? GetCustomerById(int CusId)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).ThenInclude(c => c.driver).Include(u => u.cars).ThenInclude(c => c.created_by).FirstOrDefault(c => c.Id == CusId);
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public CustomerModel? GetCustomerByPhoneNumber(string phone_number)
        {
            try
            {
                return db.Customers.Include(c => c.created_by).Include(c => c.cars).ThenInclude(c => c.driver).Include(u => u.cars).ThenInclude(c => c.created_by).FirstOrDefault(c => c.cphone_number == phone_number);
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void UpdateCustomer(CustomerModel customer)
        {
            try
            {
                customer.search = $"{customer.cfname} {customer.clname} {customer.cphone_number}";
                db.Customers.Update(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void DeleteCustomer(CustomerModel customer)
        {
            try
            {
                db.Customers.Remove(customer);
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
