using MecuryProduct.Components.Admin.Pages;
using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Employee.Pages
{
    public partial class Calendar
    {
        public List<CarModel> cars = new List<CarModel>();
        public bool mapView = false;
        public DateTime start_date = DateTime.Today.Date;
        public DateTime end_date = DateTime.Today.Date;

        /// <summary>Injects the CarService and DialogService dependencies.</summary>
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// This method is called when the object is initialized.
        /// It triggers the retrieval of cars.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCars();
        }

        /// <summary>
        /// Changes the view mode between map view and another view.
        /// </summary>
        /// <param name="isMapView">A boolean indicating whether to switch to map view or not.</param>
        public async void changeView(bool isMapView)
        {
            mapView = isMapView;
        }

        /// <summary>
        /// Opens a modal dialog to update a vehicle based on the selected appointment.
        /// </summary>
        /// <param name="args">The event arguments containing the selected car model.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<CarModel> args)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", args.Data.Id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Retrieves a list of cars from the CarService and stores them in the 'cars' list.
        /// </summary>
        public async void GetCars()
        {
            var company = await SessionService.Get<int>("company");
            cars = CarService.GetCarsByCompanyId(company).ToList();
        }
    }
}
