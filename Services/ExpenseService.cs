using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class ExpenseService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public ExpenseService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public List<ExpenseModel>? GetExpenses()
        {
            try
            {
                return db.Expenses.Include(c => c.customer).Include(c => c.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ExpenseModel>? GetExpensesByManagerId(string ManagerId)
        {
            try
            {
                return db.Expenses.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company.CompanyManagers.Any(x => x.manager_id == ManagerId)).Include(c => c.customer).Include(c => c.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ExpenseModel>? GetExpensesByCompanyId(int company_id)
        {
            try
            {
                return db.Expenses.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company_id == company_id).Include(c => c.customer).Include(c => c.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public async Task<List<ExpenseModel>?> GetExpensesByCompanyIdWithDate(int company_id, DateTime start_date, DateTime end_date)
        {
            try
            {
                return db.Expenses.Where(x => x.created_at >= start_date && x.created_at <= end_date).Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company_id == company_id).Include(c => c.customer).Include(c => c.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ExpenseModel>? GetExpensesByEmplyeeId(string EmployeeId)
        {
            try
            {
                return db.Expenses.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(c => c.company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).Include(c => c.customer).Include(c => c.created_by).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void AddExpense(ExpenseModel expense)
        {
            try
            {
                db.Expenses.Add(expense);
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
