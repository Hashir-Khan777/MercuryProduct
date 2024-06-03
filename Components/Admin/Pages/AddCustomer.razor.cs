using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;
using System.Text.Json;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddCustomer
    {
        public CustomerModel customer = new CustomerModel();
        public Root relatedAddresses { get; set; } = new Root();
        public List<string> customer_types = new List<string>()
        {
            "Phone",
            "Web",
            "Walk In",
            "Bulk"
        };
        public List<string> number_types = new List<string>()
        {
            "Cell",
            "Home",
            "Office",
        };
        public IEnumerable<string> contact_prefrence = new string[]
        {
            "Email",
            "Phone",
            "Text"
        };
        public IEnumerable<string> selected_contact_prefrence = new string[] { };
        public string email_regex = "^(([^<>()[\\]\\\\.,;:\\s@\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\"]+)*)|.(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$";

        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private ApiService ApiService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private NotificationService NotificationService { get; set; }

        protected override async void OnInitialized()
        {
            SetUserId();
            var result = await SessionService.Get<CustomerModel>("customer_form");

            if (result != null)
            {
                customer = result;
            }
        }

        public async Task OpenAddVehicleModal(int CusId)
        {
            await DialogService.OpenAsync<AddVehicleModal>("Add Vehicle",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        // PP-66: cache info before save the table and clear cache upon commit
        // Feature: Save form on cheche and clear cheche after comit
        // Fix: I have added this function to save data on cheche and call this function on change event on every field of every form
        public async void SetInSession()
        {
            await SessionService.Set("customer_form", JsonSerializer.Serialize(customer));
        }

        public async void CreateCustomer()
        {
            // PP-90 & 105: customer should not replicate
            // Bug: customer is replicating
            // Fix: Add condition on phone number if customer with same phone number exists so it wil open the update customer mosal
            var exists = CustomerService.GetCustomerByPhoneNumber(customer.cphone_number);
            if (exists == null)
            {
                customer.created_at = DateTime.UtcNow;
                customer.updated_at = DateTime.UtcNow;
                customer.contact_prefrence = selected_contact_prefrence.ToList();
                CustomerService.AddCustomer(customer);
                await SessionService.Clear("customer_form");
                await OpenAddVehicleModal(customer.Id);
                NavigationManager.NavigateTo("/admin/customers");
            }
            else
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = "Customer with this phone number already exists", Duration = 4000 };
                NotificationService.Notify(notificationMessage);
                await DialogService.OpenAsync<UpdateCustomerModal>("Update Customer",
                    new Dictionary<string, object>() { { "CusId", exists.Id } },
                    new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
                );
                StateHasChanged();
            }
        }

        public async void searchAddress(LoadDataArgs args)
        {
            var response = await ApiService.GetFromApiAsync($"https://api.tomtom.com/search/2/search/{args.Filter}.json?key=FAywZGZYK8dXtjREG8KFziDuedaBFSjb&limit=40&typeahead=true&countrySet=USA");
            var data = JsonSerializer.Deserialize<Root>(response);
            if (data is not null)
            {
                // PP-88: address validation issue
                // Bug: getting invalid address
                // Fix: Add condition on postal code if postal code is null it's invalid else it's valid
                data.Results = data.Results.FindAll(r => r.Address.PostalCode != null);
                relatedAddresses = data;
                StateHasChanged();
            }
        }

        public void OnChange(dynamic args)
        {
            Result address = relatedAddresses.Results.Find(a => a.Address.FreeformAddress == args);
            if (address != null)
            {
                customer.czip_code = address.Address.PostalCode;
                customer.ccountry = address.Address.Country;
                customer.cstate = address.Address.Municipality;
                customer.ccity = address.Address.CountrySubdivisionName;
                customer.clat = address.Position.Lat;
                customer.clon = address.Position.Lon;
            }
            SetInSession();
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
                    customer.created_by_id = userId;
                }
            }
        }
    }
}
