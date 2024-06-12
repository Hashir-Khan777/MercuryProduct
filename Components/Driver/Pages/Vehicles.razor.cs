using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Driver.Pages
{
    public partial class Vehicles
    {
        private List<CarModel> cars = new List<CarModel>();

        /// <summary>Injects dependencies for the current class.</summary>
        /// <remarks>
        /// Dependencies injected:
        /// - CarService: Service for managing car-related operations.
        /// - AuthenticationStateProvider: Provider for managing authentication state.
        /// - DialogService: Service for displaying dialogs.
        /// </remarks>
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        /// <summary>
        /// This method is called when the object is initialized.
        /// It triggers the retrieval of cars.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCars();
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
            GetCars();
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

        /// <summary>
        /// Composes a message to be sent via SMS to a specified phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number to which the message will be sent.</param>
        /// <returns>A formatted string representing an SMS message with a predefined body.</returns>
        public string SendMessage(string phoneNumber)
        {
            return $"sms:{phoneNumber}/?body=Hello! I am a rider of Zini Technologies, I am comming to pick your vehicle in 20 to 30 minutes.";
        }

        /// <summary>
        /// Retrieves the cars associated with the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method first checks if the user is authenticated, then retrieves the user's ID.
        /// It then fetches the cars associated with the user ID that are not marked as "Bought".
        /// </remarks>
        public async void GetCars()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    // PP-94: when bought vehicle should remove from driver view
                    // Bug: Bought vehicle should not show to driver
                    // Fix Add a condition on status of vehicle weather it's bought or not
                    cars = CarService.GetCarsByDriverId(userId).ToList().FindAll(c => c.status != "Bought");
                }
            }
        }

        /// <summary>
        /// Generates a full URL path based on the provided address.
        /// </summary>
        /// <param name="address">The address to be appended to the base URL.</param>
        /// <returns>A string representing the full URL path.</returns>
        public string GetPath(string address)
        {
            return $"http://maps.google.com/?q={address}";
        }
    }
}
