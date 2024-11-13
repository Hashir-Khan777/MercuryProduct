using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Radzen;

namespace MecuryProduct.Modals
{
    public partial class AddPosCustomer
    {
        public PosCustomerModel customer = new PosCustomerModel();
        public List<CompanyModel> companies = new List<CompanyModel>();
        public Root relatedAddresses { get; set; } = new Root();

        [Inject]
        private PosCustomerService PosCustomerService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private ApiService ApiService { get; set; }

        protected override async void OnInitialized()
        {
            SetUserId();
            var result = await SessionService.Get<PosCustomerModel>("pos_customer_form");

            companies = CompanyService.GetCompanies();

            if (result != null)
            {
                customer = result;
            }
        }

        public async void CreateCustomer()
        {
            customer.created_at = DateTime.UtcNow;
            customer.updated_at = DateTime.UtcNow;
            customer.CompanyId = await SessionService.Get<int>("company");
            PosCustomerService.AddCustomer(customer);
            dialogService.Close();
        }

        public async void SetInSession()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            await SessionService.Set("pos_customer_form", JsonSerializer.Serialize(customer, options));
        }

        public async void searchAddress(LoadDataArgs args)
        {
            var response = await ApiService.GetFromApiAsync($"https://api.tomtom.com/search/2/search/{args.Filter}.json?key=FAywZGZYK8dXtjREG8KFziDuedaBFSjb&limit=40&typeahead=true&countrySet=USA");
            var data = JsonSerializer.Deserialize<Root>(response);
            if (data is not null)
            {
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
