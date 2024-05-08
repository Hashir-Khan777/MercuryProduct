using MecuryProduct.Data;
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

        [Inject]
        private CarService CarService {  get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        protected override void OnInitialized()
        {
            GetCars();
        }

        public async void OpenUpdateVehicleModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "id", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async Task OpenVehicleCommentModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>("Notes",
                new Dictionary<string, object>() { { "VehId", VehId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        public async void GetCars()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    cars = CarService.GetCarsByDriverId(userId).ToList();
                }
            }
        }
    }
}
