using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class PosCustomers
    {
        public List<PosCustomerModel> pos_customers = new List<PosCustomerModel>();

        [Inject]
        private PosCustomerService PosCustomerService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        protected override async void OnInitialized()
        {
            var company = await SessionService.Get<int>("company");
            pos_customers = PosCustomerService.GetCustomersByCompanyId(company);

            base.OnInitialized();
        }

        public async void AddCustomer()
        {
            await DialogService.OpenAsync<AddPosCustomer>("Add Customer In POS",
                new Dictionary<string, object>() { },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            pos_customers = PosCustomerService.GetCustomers();
            StateHasChanged();
        }

        public async void ViewSalesHistory(int CusId)
        {
            await DialogService.OpenAsync<ViewSalesHistory>("Sales History",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "900px", Height = "90%", Resizable = true, Draggable = true }
            );
            pos_customers = PosCustomerService.GetCustomers();
            StateHasChanged();
        }

        public async void OpenUpdateCustomerModal(int cusId)
        {
            await DialogService.OpenAsync<UpdatePosCustomer>("Update Customer",
                new Dictionary<string, object>() { { "CusId", cusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            pos_customers = PosCustomerService.GetCustomers();
            StateHasChanged();
        }

        public async void DeleteCustomer(PosCustomerModel customer)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to delete customer?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                PosCustomerService.DeleteCustomer(customer);
                var company = await SessionService.Get<int>("company");
                pos_customers = PosCustomerService.GetCustomersByCompanyId(company);
                StateHasChanged();
            }
        }
    }
}
