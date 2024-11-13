using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;
using System.Reflection;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
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

        /// <summary>Injects dependencies for the current component.</summary>
        /// <remarks>
        /// Injects the CustomerService, DialogService, and IJSRuntime dependencies into the current component.
        /// </remarks>
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// Method called after the component has been rendered.
        /// </summary>
        /// <param name="firstRender">A boolean indicating if this is the first render of the component.</param>
        /// <returns>An asynchronous Task.</returns>
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

        /// <summary>
        /// This method is called when the component's parameters are set.
        /// It retrieves customers, filters them based on scheduled dates, and initializes the map accordingly.
        /// </summary>
        /// <remarks>
        /// If it's not the first render, it fetches customers, filters them based on scheduled dates within a specified range,
        /// calculates the center coordinates of the filtered customers, initializes the map with the center coordinates, and adds markers.
        /// If no customers match the filter criteria, it initializes the map without any markers.
        /// </remarks>
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

        /// <summary>
        /// Retrieves a list of customers from the CustomerService and stores them in the 'customers' field.
        /// </summary>
        public async void GetCustomers()
        {
            var company = await SessionService.Get<int>("company");
            customers = CustomerService.GetCustomersByCompanyId(company);
        }

        /// <summary>Adds markers for customers based on their cars' scheduled dates and status.</summary>
        /// <remarks>
        /// This method iterates through the list of customers and filters their cars based on the scheduled dates within a specified range.
        /// It then determines the color of the marker based on the status of the car (Scheduled, Bought, DnD).
        /// The marker is added using JavaScript interop with the specified customer details.
        /// Finally, it sets the bounds for the markers on the map if there are customers present.
        /// </remarks>
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

        /// <summary>
        /// Handles the click event on a marker and opens a modal dialog to show customer vehicles.
        /// </summary>
        /// <param name="id">The ID of the marker clicked.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
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
