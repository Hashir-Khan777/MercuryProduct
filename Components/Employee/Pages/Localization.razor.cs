using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Employee.Pages
{
    public partial class Localization
    {
        [Inject]
        private LocalizationService LocalizationService { get; set; }
        [Inject]
        private NotificationService NotificationService { get; set; }
        [Inject]
        public CompanyService CompanyService { get; set; }
        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public LocalizationModel localization = new LocalizationModel();

        public List<string> showPrices = new List<string> { "regular_price", "custom_price_1", "custom_price_2", "custom_price_3", "custom_price_4" };

        public List<CompanyModel> companies = new List<CompanyModel>();

        public int default_company;

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            GetCompaniesByEmployeeId();
        }

        public async void GetCompaniesByEmployeeId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    companies = CompanyService.GetCompaniesByEmployeeId(userId);
                    await SessionService.Set("company", companies[0].Id.ToString());
                    var company_id = await SessionService.Get<int?>("company");

                    if (companies is not null)
                    {
                        GetLocalizationByCompanyId(company_id);
                    }

                    if (company_id is not null)
                    {
                        default_company = (int)company_id;
                    }
                    else
                    {
                        if (companies is not null)
                        {
                            await SessionService.Set("company", companies[0].Id.ToString());
                            default_company = companies[0].Id;
                        }
                    }
                }
            }
        }

        public async void SaveLocalization()
        {
            localization.company_id = await SessionService.Get<int>("company");
            LocalizationService.AddLocalization(localization);
            GetLocalizationByCompanyId(localization.company_id);
            var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Success, Detail = "Localization Setting Saved Successfully", Duration = 4000 };
            NotificationService.Notify(notificationMessage);
            StateHasChanged();
        }

        public void GetLocalizationByCompanyId(int? companyId)
        {
            if (companyId is not null)
            {
                var getlocalsettings = LocalizationService.GetLocalizationByCompanyId(companyId);
                if (getlocalsettings is not null)
                {
                    localization = getlocalsettings;
                }
            }
        }

        public async void ChangeCompany(dynamic item)
        {
            await SessionService.Set("company", item.ToString());
            default_company = item;
            NavigationManager.Refresh(true);
            StateHasChanged();
        }

        public async void changeProductImage(dynamic e)
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"default-{datetime}-{file.Name}";
                await using (var stream = file.OpenReadStream(long.MaxValue))
                {
                    await using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
                localization.defaultProductImage = "uploads/" + $"default-{datetime}-{file.Name}";
                StateHasChanged();
            }
        }

        public async void changeCompanyLogo(dynamic e)
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"companylogo-{datetime}-{file.Name}";
                await using (var stream = file.OpenReadStream(long.MaxValue))
                {
                    await using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
                localization.companyLogo = "uploads/" + $"companylogo-{datetime}-{file.Name}";
                StateHasChanged();
            }
        }
    }
}
