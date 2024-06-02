using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Reflection;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Map
    {
        [Parameter]
        public DateTime start_date { get; set; }
        [Parameter]
        public DateTime end_date { get; set; }

        public List<CustomerModel> customers = new List<CustomerModel>();
        private static Map _app;
        public bool first_render = true;

        public Map()
        {
            _app = this;
        }

        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                GetCustomers();
                StateHasChanged();
                if (customers.Count() > 0)
                {
                    var centerLat = (customers[0].clat + customers[customers.Count() - 1].clat) / 2;
                    var centerLng = (customers[0].clon + customers[customers.Count() - 1].clon) / 2;
                    await JS.InvokeVoidAsync("initMap", centerLat, centerLng);
                    AddMarkers();
                }
                first_render = false;
            }
        }

        protected override async void OnParametersSet()
        {
            if (!first_render)
            {
                GetCustomers();
                StateHasChanged();
                List<CustomerModel> filteredCustomers = customers.FindAll(c => c.cars?.FindAll(c => c.scheduled_date.Date >= start_date.Date && c.scheduled_date.Date <= end_date.Date).Count() > 0);
                if (filteredCustomers.Count() > 0)
                {
                    var centerLat = (filteredCustomers[0].clat + filteredCustomers[filteredCustomers.Count() - 1].clat) / 2;
                    var centerLng = (filteredCustomers[0].clon + filteredCustomers[filteredCustomers.Count() - 1].clon) / 2;
                    await JS.InvokeVoidAsync("initMap", centerLat, centerLng);
                    AddMarkers();
                }
                else
                {
                    await JS.InvokeVoidAsync("initMap");
                }
            }
        }

        public void GetCustomers()
        {
            customers = CustomerService.GetCustomers().ToList();
        }

        public async void AddMarkers()
        {
            foreach (var customer in customers)
            {
                List<CarModel> cars = customer.cars.FindAll(c => c.scheduled_date.Date >= start_date.Date && c.scheduled_date.Date <= end_date.Date);
                if (cars?.Count() > 0)
                {
                    string color = customer.cars?.Find(c => c.status == "Scheduled") != null ? "red" : customer.cars?.Find(c => c.status == "Bought") != null ? "green" : customer.cars?.Find(c => c.status == "DnD") != null ? "black" : "red";
                    string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                    await JS.InvokeVoidAsync("addMarker", customer?.clon, customer?.clat, customer?.Id, assemblyName, color, customer?.cfname + " " + customer?.clname, customer?.caddress, customer?.cphone_number);
                }
            }
            if (customers.Count() > 0)
            {
                await JS.InvokeVoidAsync("setBounds");
            }
        }

        [JSInvokable("OnMarkerClick")]
        public static async void OnMarkerClick(int id)
        {
            await _app.DialogService.OpenAsync<ShowCustomerVehiclesModal>("Vehicles",
                new Dictionary<string, object>() { { "Id", id }, { "Role", "customer" }, { "start_date", _app.start_date }, { "end_date", _app.end_date } },
                new DialogOptions() { Width = "90%", Height = "70%", Resizable = true, Draggable = true }
            );
        }
    }
}
