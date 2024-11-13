using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public PaymentService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public void AddPayment(PaymentModel payment)
        {
            try
            {
                db.Payments.Add(payment);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<PaymentModel>? SearchPayment(string query)
        {
            try
            {
                return db.Payments.Include(c => c.customer).Where(c => c.customer.cfname.Contains(query) || c.customer.clname.Contains(query)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? SearchPaymentByManagerId(string query, string ManagerId)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company.CompanyManagers.Any(x => x.manager_id == ManagerId)).Include(c => c.customer).Where(c => c.customer.cfname.Contains(query) || c.customer.clname.Contains(query)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? SearchPaymentByCompanyId(string query, int company_id)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company_id == company_id).Include(c => c.customer).Where(c => c.customer.cfname.Contains(query) || c.customer.clname.Contains(query)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? SearchPaymentByEmployeeId(string query, string EmployeeId)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).Include(c => c.customer).Where(c => c.customer.cfname.Contains(query) || c.customer.clname.Contains(query)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? GetPayments()
        {
            try
            {
                return db.Payments.Include(p => p.customer).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? GetPaymnetsByManagerId(string ManagerId)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company.CompanyManagers.Any(x => x.manager_id == ManagerId)).Include(c => c.customer).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? GetPaymnetsByCompanyId(int company_id)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company_id == company_id).Include(c => c.customer).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public async Task<List<PaymentModel>?> GetPaymnetsByCompanyIdWithTime(int company_id, DateTime start_date, DateTime end_date)
        {
            try
            {
                return db.Payments.Where(x => x.created_at >= start_date && x.created_at <= end_date).Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company_id == company_id).Include(c => c.customer).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? GetPaymnetsByCustomerId(int cus_id)
        {
            try
            {
                return db.Payments.Include(c => c.company).Include(c => c.customer).Where(x => x.customerId == cus_id).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? GetPaymnetsByEmployeeId(string EmployeeId)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).Include(c => c.customer).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<PaymentModel>? GetPaymentsByEmployeeId(string EmployeeId)
        {
            try
            {
                return db.Payments.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).Include(c => c.customer).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public PaymentModel? GetPaymentById(int PaymentID)
        {
            try
            {
                return db.Payments.Include(p => p.customer).Include(p => p.company).FirstOrDefault(x => x.Id == PaymentID);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void UpdatePayment(PaymentModel item)
        {
            try
            {
                db.Payments.Update(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void DeletePayment(PaymentModel item)
        {
            try
            {
                db.Payments.Remove(item);
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
