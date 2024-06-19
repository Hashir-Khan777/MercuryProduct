using MecuryProduct.Components.Admin.Pages;
using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class AddCompany
    {
        public CompanyModel company = new CompanyModel();
        public List<ApplicationUser> managers = new List<ApplicationUser>();

        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async void OnInitialized()
        {
            GetManagers();
            SetUserId();

            var result = await SessionService.Get<CompanyModel>("company_form");

            if (result != null)
            {
                company = result;
            }
        }

        public void CreateCompany(CompanyModel company)
        {
            CompanyService.AddCompany(company);
            NavigationManager.NavigateTo("/manager/companies");
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
                    company.ManagerId = userId;
                }
            }
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
