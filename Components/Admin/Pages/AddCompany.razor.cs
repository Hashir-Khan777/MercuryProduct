using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddCompany
    {
        public CompanyModel company = new CompanyModel();
        public List<ApplicationUser> managers = new List<ApplicationUser>();
        public List<string> selected_managers = new List<string>();

        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

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

        public async void SetInSession()
        {
            await SessionService.Set("company_form", JsonSerializer.Serialize(company));
        }

        public void GetManagers()
        {
            managers = UserService.GetUsersByClaim("Role", "Manager");
        }
    }
}
