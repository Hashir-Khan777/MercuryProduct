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
        }

        public void UpdateCompany(CompanyModel company)
        {
            CompanyService.Update(company);
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
                    if (role == "Manager")
                    {
                        company.ManagerId = userId;
                    }
                }
            }
        }
    }
}
