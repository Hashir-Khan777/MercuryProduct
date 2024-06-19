using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class CompanyService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public CompanyService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public List<CompanyModel>? GetCompanies()
        {
            try
            {
                return db.Companies.Include(c => c.Manager).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<CompanyModel>? GetCompaniesByManagerId(string ManagerId)
        {
            try
            {
                return db.Companies.Where(c => c.ManagerId == ManagerId).Include(c => c.Manager).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public CompanyModel? GetCompanyById(int CompId)
        {
            try
            {
                return db.Companies.FirstOrDefault(c => c.Id == CompId);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void AddCompany(CompanyModel company)
        {
            try
            {
                db.Companies.Add(company);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void Remove(CompanyModel company)
        {
            try
            {
                db.Companies.Remove(company);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void Update(CompanyModel company)
        {
            try
            {
                db.Companies.Update(company);
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
