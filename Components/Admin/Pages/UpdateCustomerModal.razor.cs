using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class UpdateCustomerModal
    {
        [Parameter] public int cusId {  get; set; }

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
        private ApiService ApiService { get; set; }

        protected override void OnInitialized()
        {
            GetCustomerById();
        }

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

        public void GetCustomerById()
        {
            var result = CustomerService.GetCustomerById(cusId);
            if (result != null)
            {
                customer = result;
                selected_contact_prefrence = result.contact_prefrence;
            }
        }

        public void UpdateCustomer()
        {
            customer.contact_prefrence = selected_contact_prefrence.ToList();
            customer.updated_at = DateTime.UtcNow;
            CustomerService.UpdateCustomer(customer);
            dialogService.Close();
        }
    }
}
