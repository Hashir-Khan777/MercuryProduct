using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class ShowCustomerVehiclesModal
    {
        [Parameter] public Object Id {  get; set; }
        [Parameter] public string Role { get; set; }

        public CustomerModel customer = new CustomerModel();
        public ApplicationUser driver = new ApplicationUser();

        [Inject]
        public CustomerService CustomerService {  get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private UserService DriverService { get; set; }

        protected override void OnInitialized()
        {
            GetCustomerById();
        }

        public void GetCustomerById()
        {
            if (Role.ToLower() == "customer")
            {
                var result = CustomerService.GetCustomerById((int)Id);
                if (result != null)
                {
                    customer = result;
                }
            }
            else
            {
                var result = DriverService.GetUserById((string)Id);
                if (result != null)
                {
                    driver = result;
                }
            }
        }

        public async void OpenUpdateCustomerModal(int id)
        {
            await DialogService.OpenAsync<UpdateCustomerModal>("Update Customer",
                new Dictionary<string, object>() { { "CusId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void OpenUpdateDriverModal(string id)
        {
            await DialogService.OpenAsync<UpdateDriverModal>("Update Driver",
                new Dictionary<string, object>() { { "DriverId", id } },
                new DialogOptions() { Width = "600px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void DeleteVehicle(CarModel car)
        {
            bool? deleteVehicle = await DialogService.Confirm("Are you sure?", "Do you want to delete vehicle?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteVehicle != null && deleteVehicle == true)
            {
                CarService.DeleteCar(car);
                StateHasChanged();
            }
        }

        public async void OpenUpdateVehicleModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id } },
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
    }
}
