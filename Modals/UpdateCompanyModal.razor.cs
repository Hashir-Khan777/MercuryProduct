using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Modals
{
    public partial class UpdateCompanyModal
    {
        [Parameter] public int CompId { get; set; }
        public CompanyModel company = new CompanyModel();
        public List<ApplicationUser> managers = new List<ApplicationUser>();

        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private UserService UserService { get; set; }

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
    }
}
