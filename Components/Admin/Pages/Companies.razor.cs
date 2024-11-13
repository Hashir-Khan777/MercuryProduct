using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Companies
    {
        public List<CompanyModel> companies = new List<CompanyModel>();

        [Inject]
        private CompanyService CompanyService {  get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GetCompanies();
        }

        public void GetCompanies()
        {
            companies = CompanyService.GetCompanies();
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
