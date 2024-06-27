using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Modals
{
    public partial class UpdateCompanyModal
    {
        [Parameter] public int CompId { get; set; }
        public CompanyModel company = new CompanyModel();
        public List<ApplicationUser> managers = new List<ApplicationUser>();
        public List<string> selected_managers = new List<string>();
        public string user_role = string.Empty;

        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async void OnInitialized()
        {
            GetManagers();
            GetCompanyById();
        }

        public void GetCompanyById()
        {
            company = CompanyService.GetCompanyById(CompId);
            foreach (var item in company.CompanyManagers)
            {
                selected_managers.Add(item.manager_id);
            }
        }

        public void UpdateCompany(CompanyModel company)
        {
            CompanyService.Update(company);
            foreach (var manager in selected_managers)
            {
                CompanyService.AddManager(new CompanyManager { company_id = company.Id, manager_id = manager });
            }
            var tobedeleted = managers.Where(m => !selected_managers.Contains(m.Id));
            foreach (var item in tobedeleted)
            {
                CompanyService.DeleteManager(new CompanyManager { company_id = company.Id, manager_id = item.Id });
            }
            dialogService.Close();
        }

        public void GetManagers()
        {
            managers = UserService.GetUsersByClaim("Role", "Manager");
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    var role = UserService.GetUserClaimByUserId(userId);
                    user_role = role;
                    //if (role == "Manager")
                    //{
                    //    company.ManagerId = userId;
                    //}
                }
            }
        }
    }
}
