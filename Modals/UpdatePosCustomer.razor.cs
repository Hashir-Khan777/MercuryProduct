using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MecuryProduct.Modals
{
    public partial class UpdatePosCustomer
    {
        [Parameter] public int CusId {  get; set; }

        public PosCustomerModel customer = new PosCustomerModel();
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
            customer = PosCustomerService.GetCustomerById(CusId);
        }

        public async void CreateCustomer()
        {
            customer.updated_at = DateTime.UtcNow;
            PosCustomerService.UpadateCustomer(customer);
            dialogService.Close();
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
        }
    }
}
