using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Driver
    {
        private List<ApplicationUser> drivers = new List<ApplicationUser>();

        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        protected override async void OnInitialized()
        {
            GetDrivers();
        }

        public void GetDrivers()
        {
            drivers = DriverService.GetUsersByClaim("Role", "Driver").ToList();
        }

        public async void OpenUpdateDriverModal(string id)
        {
            await DialogService.OpenAsync<UpdateDriverModal>("Update Driver",
                new Dictionary<string, object>() { { "DriverId", id } },
                new DialogOptions() { Width = "600px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async Task OpenShowVehiclesModal(string Id)
        {
            await DialogService.OpenAsync<ShowCustomerVehiclesModal>("Vehicles",
                new Dictionary<string, object>() { { "Id", Id }, { "Role", "driver" } },
                new DialogOptions() { Width = "90%", Height = "70%", Resizable = true, Draggable = true }
            );
        }

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
