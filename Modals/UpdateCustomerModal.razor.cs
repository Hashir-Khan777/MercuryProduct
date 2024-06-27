using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;
using System.Text.Json;

namespace MecuryProduct.Modals
{
    public partial class UpdateCustomerModal
    {
        [Parameter] public int cusId { get; set; }

        public CustomerModel customer = new CustomerModel();
        public Root relatedAddresses { get; set; } = new Root();
        public List<CompanyModel> companies = new List<CompanyModel>();
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
        public string user_role = string.Empty;

        /// <summary>Injects the CustomerService and ApiService dependencies.</summary>
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private ApiService ApiService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        /// <summary>
        /// This method is called when the element is initialized.
        /// It retrieves a customer by their ID.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCompanyByUserId();
            GetCustomerById();
        }

        /// <summary>
        /// Searches for an address based on the provided arguments by making an asynchronous call to the API.
        /// </summary>
        /// <param name="args">The arguments used to load the data for the address search.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async void searchAddress(LoadDataArgs args)
        {
            var response = await ApiService.GetFromApiAsync($"https://api.tomtom.com/search/2/search/{args.Filter}.json?key=FAywZGZYK8dXtjREG8KFziDuedaBFSjb&limit=40&typeahead=true&countrySet=USA");
            var data = JsonSerializer.Deserialize<Root>(response);
            if (data is not null)
            {
                relatedAddresses = data;
                StateHasChanged();
            }
        }

        public async void GetCompanyByUserId()
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
                        companies = CompanyService.GetCompaniesByManagerId(userId);
                    }
                    else if (role == "Employee")
                    {
                        companies = CompanyService.GetCompaniesByEmployeeId(userId);
                    }
                    else
                    {
                        companies = CompanyService.GetCompanies();
                    }
                }
            }
        }

        /// <summary>
        /// Updates customer address information based on the provided address.
        /// </summary>
        /// <param name="args">The address to look for in the related addresses.</param>
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
        }

        /// <summary>
        /// Retrieves a customer by their ID and updates the customer and selected contact preference fields.
        /// </summary>
        /// <remarks>
        /// This method fetches a customer object from the CustomerService based on the provided customer ID.
        /// If a customer is found, it updates the 'customer' field with the retrieved customer object
        /// and the 'selected_contact_prefrence' field with the contact preference of the customer.
        /// </remarks>
        public void GetCustomerById()
        {
            var result = CustomerService.GetCustomerById(cusId);
            if (result != null)
            {
                customer = result;
                selected_contact_prefrence = result.contact_prefrence;
            }
        }

        /// <summary>
        /// Updates the customer's contact preference and last updated timestamp, then calls the CustomerService to update the customer.
        /// </summary>
        /// <remarks>
        /// This method updates the customer's contact preference based on the selected preferences, sets the updated timestamp to the current UTC time,
        /// and then triggers the CustomerService to update the customer with the new information. Finally, it closes the dialog service.
        /// </remarks>
        public void UpdateCustomer()
        {
            customer.contact_prefrence = selected_contact_prefrence.ToList();
            customer.updated_at = DateTime.UtcNow;
            CustomerService.UpdateCustomer(customer);
            dialogService.Close();
        }
    }
}
