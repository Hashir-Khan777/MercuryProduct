using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddCompany
    {
        public CompanyModel company = new CompanyModel();
        public List<ApplicationUser> managers = new List<ApplicationUser>();
        public List<string> selected_managers = new List<string>();
        public Root relatedAddresses { get; set; } = new Root();

        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private ApiService ApiService { get; set; }

        protected override async void OnInitialized()
        {
            GetManagers();

            var result = await SessionService.Get<CompanyModel>("company_form");

            if (result != null)
            {
                company = result;
            }
        }

        public void CreateCompany(CompanyModel company)
        {
            CompanyService.AddCompany(company);
            foreach (var manager in selected_managers)
            {
                CompanyService.AddManager(new CompanyManager { company_id = company.Id, manager_id = manager });
            }
            NavigationManager.NavigateTo("/admin/companies");
        }

        public async void searchAddress(LoadDataArgs args)
        {
            var response = await ApiService.GetFromApiAsync($"https://api.tomtom.com/search/2/search/{args.Filter}.json?key=FAywZGZYK8dXtjREG8KFziDuedaBFSjb&limit=40&typeahead=true&countrySet=USA");
            var data = JsonSerializer.Deserialize<Root>(response);
            if (data is not null)
            {
                data.Results = data.Results.FindAll(r => r.Address.PostalCode != null);
                relatedAddresses = data;
                StateHasChanged();
            }
        }

        public void OnChange(dynamic args)
        {
            Result address = relatedAddresses.Results.Find(a => a.Address.FreeformAddress == args);
            if (address != null)
            {
                company.czip_code = address.Address.PostalCode;
                company.ccountry = address.Address.Country;
                company.cstate = address.Address.Municipality;
                company.ccity = address.Address.CountrySubdivisionName;
                company.clat = address.Position.Lat;
                company.clon = address.Position.Lon;
            }
            SetInSession();
        }

        public async void SetInSession()
        {
            await SessionService.Set("company_form", JsonSerializer.Serialize(company));
        }

        public async void GetManagers()
        {
            managers = UserService.GetUsersByClaim("Role", "Manager");
        }
    }
}
