using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class PosCustomerService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public PosCustomerService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public void AddCustomer(PosCustomerModel customer)
        {
            try
            {
                customer.search = $"{customer.cfname} {customer.clname} {customer.cphone_number}";
                db.PosCustomers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void UpadateCustomer(PosCustomerModel customer)
        {
            try
            {
                customer.search = $"{customer.cfname} {customer.clname} {customer.cphone_number}";
                db.PosCustomers.Update(customer);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<PosCustomerModel>? GetCustomers()
        {
            try
            {
                return db.PosCustomers.Include(c => c.created_by).Include(c => c.Company).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public PosCustomerModel? GetCustomerById(int cusId)
        {
            try
            {
                return db.PosCustomers.Include(c => c.created_by).Include(c => c.Company).OrderByDescending(c => c.created_at).FirstOrDefault(x => x.Id == cusId);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PosCustomerModel>? GetCustomersByCompanyId(int company_id)
        {
            try
            {
                return db.PosCustomers.Include(c => c.created_by).Include(c => c.Company).ThenInclude(c => c.CompanyManagers).Where(c => c.CompanyId == company_id).OrderByDescending(c => c.created_at).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void DeleteCustomer(PosCustomerModel customer)
        {
            try
            {
                db.PosCustomers.Remove(customer);
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
