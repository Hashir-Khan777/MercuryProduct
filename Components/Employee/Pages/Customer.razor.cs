using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Employee.Pages
{
    public partial class Customer
    {
        private List<CustomerModel> customers = new List<CustomerModel>();

        /// <summary>Injects the CustomerService and DialogService dependencies.</summary>
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// This method is called when the object is initialized.
        /// It triggers the retrieval of customer data.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCustomers();
        }

        /// <summary>
        /// Retrieves a list of customers from the CustomerService and stores them in the 'customers' list.
        /// </summary>
        public async void GetCustomers()
        {
            var company = await SessionService.Get<int>("company");
            customers = CustomerService.GetCustomersByCompanyId(company).ToList();
        }

        /// <summary>Opens a modal dialog to add a vehicle for a specific customer.</summary>
        /// <param name="CusId">The ID of the customer for whom the vehicle is being added.</param>
        /// <returns>An asynchronous task representing the operation.</returns>
        public async Task OpenAddVehicleModal(int CusId)
        {
            await DialogService.OpenAsync<AddVehicleModal>("Add Vehicle",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>
        /// Opens a modal dialog to show vehicles associated with a customer.
        /// </summary>
        /// <param name="CusId">The ID of the customer whose vehicles are to be displayed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenShowVehiclesModal(int CusId)
        {
            await DialogService.OpenAsync<ShowCustomerVehiclesModal>("Vehicles",
                new Dictionary<string, object>() { { "Id", CusId }, { "Role", "customer" } },
                new DialogOptions() { Width = "90%", Height = "70%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>
        /// Opens a modal dialog to update customer information.
        /// </summary>
        /// <param name="CusId">The ID of the customer to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateCustomerModal(int CusId)
        {
            await DialogService.OpenAsync<UpdateCustomerModal>("Update Customer",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Deletes a customer after confirming with the user.
        /// </summary>
        /// <param name="customer">The customer to be deleted.</param>
        /// <returns>Void</returns>
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
