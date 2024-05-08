using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class ShowCustomerVehiclesModal
    {
        [Parameter] public int cusId {  get; set; }

        public CustomerModel customer = new CustomerModel();

        [Inject]
        public CustomerService CustomerService {  get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private CarService CarService { get; set; }

        protected override void OnInitialized()
        {
            GetCustomerById();
        }

        public void GetCustomerById()
        {
            var result = CustomerService.GetCustomerById(cusId);
            if (result != null)
            {
                customer = result;
            }
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
