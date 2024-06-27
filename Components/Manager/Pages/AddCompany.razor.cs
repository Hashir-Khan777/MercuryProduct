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
        public List<string> selected_managers = new List<string>();
        public string user_id = string.Empty;

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
            SetUserId();
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
            NavigationManager.NavigateTo("/manager/companies");
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                user_id = userId;

                if (userId is not null)
                {
                    selected_managers.Add(userId);
                }
            }
        }

        public async void SetInSession()
        {
            await SessionService.Set("company_form", JsonSerializer.Serialize(company));
        }

        public void GetManagers()
        {
            var result = UserService.GetUsersByClaim("Role", "Manager");
            foreach (var item in result)
            {
                if (item.Id != user_id)
                {
                    managers.Add(item);
                }
            }
        }
    }
}
