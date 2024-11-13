using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MecuryProduct.Modals
{
    public partial class AddCustomerModal
    {
        public bool update_customer = false;
        public CustomerModel customer = new CustomerModel();
        public List<CompanyModel> companies = new List<CompanyModel>();
        public Root relatedAddresses { get; set; } = new Root();
        public List<CustomerModel> customers_by_phone_number = new List<CustomerModel>();
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

        /// <summary>
        /// Injects various services and managers required for the functionality of the application.
        /// </summary>
        /// <remarks>
        /// The injected services include CustomerService, NavigationManager, AuthenticationStateProvider,
        /// ApiService, DialogService, SessionService, and NotificationService.
        /// </remarks>
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
        [Inject]
        private HelperService HelperService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }

        /// <summary>
        /// Initializes the component and retrieves the customer data from the session service.
        /// </summary>
        /// <remarks>
        /// This method sets the user ID and then attempts to retrieve the customer data from the session service.
        /// If the data is successfully retrieved, it updates the 'customer' object with the retrieved data.
        /// </remarks>
        protected override async void OnInitialized()
        {
            SetUserId();
            var result = await SessionService.Get<CustomerModel>("customer_form");

            companies = CompanyService.GetCompanies();

            if (result != null)
            {
                customer = result;
            }
        }

        /// <summary>
        /// Opens a modal dialog to add a vehicle for a specific customer.
        /// </summary>
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
        /// Sets the customer form data in the session using JSON serialization.
        /// </summary>
        /// <remarks>
        /// This method asynchronously serializes the customer object using JSON serialization
        /// and stores it in the session with the key "customer_form".
        /// </remarks>
        // PP-66: cache info before save the table and clear cache upon commit
        // Feature: Save form on cheche and clear cheche after comit
        // Fix: I have added this function to save data on cheche and call this function on change event on every field of every form
        public async void SetInSession()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            await SessionService.Set("customer_form", JsonSerializer.Serialize(customer, options));
        }

        /// <summary>
        /// Creates a new customer with the provided details.
        /// </summary>
        /// <remarks>
        /// If a customer with the same phone number already exists, it displays an error message and allows the user to update the existing customer.
        /// </remarks>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void CreateCustomer()
        {
            if (!update_customer)
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
                    customer.CompanyId = await SessionService.Get<int>("company");
                    CustomerService.AddCustomer(customer);
                    DialogService.Close(customer.Id);
                    await SessionService.Clear("customer_form");
                }
            }
        }

        /// <summary>
        /// Searches for an address based on the provided arguments by making an asynchronous call to the API.
        /// </summary>
        /// <param name="args">The arguments used for loading data.</param>
        /// <remarks>
        /// This method sends a request to the specified API endpoint to retrieve address data.
        /// It then deserializes the response into a Root object using the System.Text.Json.JsonSerializer.
        /// If the deserialized data is not null, it filters the results to include only those with a non-null PostalCode.
        /// The filtered results are stored in the relatedAddresses field, and the UI state is updated using StateHasChanged().
        /// </remarks>
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

        public void searchPhoneNumber(LoadDataArgs args)
        {
            var filtred_customers = CustomerService.SearchCustomerListByPhoneNumber(args.Filter);
            if (filtred_customers is not null)
            {
                customers_by_phone_number = filtred_customers;
                StateHasChanged();
            }
        }

        public void changePhoneNumber(dynamic args)
        {
            var filtred_customer = customers_by_phone_number.Find(c => c.cphone_number == args);
            if (filtred_customer != null)
            {
                update_customer = true;
                customer = filtred_customer;
                SetInSession();
            }
        }

        /// <summary>
        /// Updates customer information based on the provided address.
        /// </summary>
        /// <param name="args">The address to look for in the related addresses.</param>
        /// <remarks>
        /// If the address is found in the related addresses, the customer's zip code, country, state, city, latitude, and longitude
        /// are updated accordingly. Finally, the changes are saved in the session.
        /// </remarks>
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

        /// <summary>
        /// Sets the user ID for the customer based on the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state of the user and sets the user ID for the customer
        /// if the user is authenticated. The user ID is obtained from the claim with the type ClaimTypes.NameIdentifier.
        /// </remarks>
        /// <returns>Void</returns>
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
