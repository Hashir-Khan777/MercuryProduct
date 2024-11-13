using MecuryProduct.Components.Admin.Pages;
using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class Companies
    {
        public List<CompanyModel> companies = new List<CompanyModel>();

        [Inject]
        private CompanyService CompanyService {  get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GetCompanies();
        }

        public async void GetCompanies()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    companies = CompanyService.GetCompaniesByManagerId(userId);
                }
            }
        }

        public async void OpenUpdateCompanyModal(int CompId)
        {
            await DialogService.OpenAsync<UpdateCompanyModal>("Update Company",
                new Dictionary<string, object>() { { "CompId", CompId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void DeleteCompany(CompanyModel company)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to delete company?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                CompanyService.Remove(company);
                GetCompanies();
                StateHasChanged();
            }
        }
    }
}
