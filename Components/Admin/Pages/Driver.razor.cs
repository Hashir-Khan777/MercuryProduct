using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Driver
    {
        private List<ApplicationUser> drivers = new List<ApplicationUser>();

        /// <summary>Injects the UserService and DialogService dependencies.</summary>
        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// This method is called when the object is initialized.
        /// It triggers the retrieval of drivers.
        /// </summary>
        protected override async void OnInitialized()
        {
            GetDrivers();
        }

        /// <summary>
        /// Retrieves a list of drivers from the DriverService based on a specific claim.
        /// </summary>
        /// <remarks>
        /// This method populates the 'drivers' list with users who have the claim "Role" set to "Driver".
        /// </remarks>
        public async void GetDrivers()
        {
            var company = await SessionService.Get<int>("company");
            drivers = DriverService.GetUsersByClaimByCompanyId("Role", "Driver", company);
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
        /// Opens a modal dialog to show vehicles associated with a customer.
        /// </summary>
        /// <param name="Id">The identifier of the customer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenShowVehiclesModal(string Id)
        {
            await DialogService.OpenAsync<ShowCustomerVehiclesModal>("Vehicles",
                new Dictionary<string, object>() { { "Id", Id }, { "Role", "driver" } },
                new DialogOptions() { Width = "90%", Height = "70%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>
        /// Deletes a user after confirming the action through a dialog.
        /// </summary>
        /// <param name="user">The user to be deleted.</param>
        /// <returns>Void</returns>
        public async void DeleteUser(ApplicationUser user)
        {
            bool? deleteUser = await DialogService.Confirm("Are you sure?", "Do you want to delete driver?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

            if (deleteUser != null && deleteUser == true)
            {
                DriverService.DeleteUser(user);
                GetDrivers();
                StateHasChanged();
            }
        }
    }
}
