using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Customer
    {
        private List<CustomerModel> customers = new List<CustomerModel>();

        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private DialogService DialogService {  get; set; }

        protected override void OnInitialized()
        {
            GetCustomers();
        }

        public void GetCustomers()
        {
            customers = CustomerService.GetCustomers().ToList();
        }

        public async Task OpenAddVehicleModal(int CusId)
        {
            await DialogService.OpenAsync<AddVehicleModal>("Add Vehicle",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        public async Task OpenShowVehiclesModal(int CusId)
        {
            await DialogService.OpenAsync<ShowCustomerVehiclesModal>("Vehicles",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "90%", Height = "70%", Resizable = true, Draggable = true }
            );
        }

        public async void OpenUpdateCustomerModal(int CusId)
        {
            await DialogService.OpenAsync<UpdateCustomerModal>("Update Customer",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void DeleteCustomer(CustomerModel customer)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to delete customer?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                CustomerService.DeleteCustomer(customer);
                GetCustomers();
                StateHasChanged();
            }
        }
    }
}
