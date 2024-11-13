using MecuryProduct.Data;
using Radzen;
using System.Linq.Dynamic.Core;

namespace MecuryProduct.Services
{
    public class LocalizationService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public LocalizationService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public void AddLocalization(LocalizationModel localization)
        {
            try
            {
                var alreadyExist = db.Localization.FirstOrDefault(x => x.company_id == localization.company_id);
                if (alreadyExist != null)
                {
                    db.Localization.Update(localization);
                    db.SaveChanges();
                }
                else
                {
                    db.Localization.Add(localization);
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

        public LocalizationModel? GetLocalizationByCompanyId(int? companyId)
        {
            try
            {
                return db.Localization.FirstOrDefault(x => x.company_id == companyId);
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
