using MecuryProduct.Components.Admin.Pages;
using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class Vehicles
    {
        private List<CarModel> cars = new List<CarModel>();

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
        /// It triggers the retrieval of car data.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCars();
        }

        /// <summary>
        /// Retrieves a list of cars from the CarService and stores them in the 'cars' field.
        /// </summary>
        public async void GetCars()
        {
            var company = await SessionService.Get<int>("company");
            cars = CarService.GetCarsByCompanyId(company);
        }

        /// <summary>
        /// Deletes a vehicle from the system after confirming with the user.
        /// </summary>
        /// <param name="car">The car model to be deleted.</param>
        /// <returns>Void</returns>
        /// <remarks>
        /// This method prompts the user with a confirmation dialog before deleting the vehicle.
        /// If the user confirms the deletion, the car is removed using the CarService.
        /// After deletion, the list of cars is updated by calling GetCars() and the UI state is refreshed.
        /// </remarks>
        public async void DeleteVehicle(CarModel car)
        {
            bool? deleteVehicle = await DialogService.Confirm("Are you sure?", "Do you want to delete vehicle?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteVehicle != null && deleteVehicle == true)
            {
                CarService.DeleteCar(car);
            }
            GetCars();
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to update a vehicle with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateVehicleModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to update customer information.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateCustomerModal(int id)
        {
            await DialogService.OpenAsync<UpdateCustomerModal>("Update Customer",
                new Dictionary<string, object>() { { "CusId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to update a driver with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the driver to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateDriverModal(string id)
        {
            await DialogService.OpenAsync<UpdateDriverModal>("Update Driver",
                new Dictionary<string, object>() { { "DriverId", id } },
                new DialogOptions() { Width = "600px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to display comments for a specific vehicle.
        /// </summary>
        /// <param name="VehId">The ID of the vehicle for which comments are to be displayed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenVehicleCommentModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Notes for m-veh-{VehId}",
                new Dictionary<string, object>() { { "VehId", VehId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }
    }
}
