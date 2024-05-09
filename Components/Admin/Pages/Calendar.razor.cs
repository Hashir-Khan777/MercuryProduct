using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Calendar
    {
        public List<CarModel> cars = new List<CarModel>();

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        protected override void OnInitialized()
        {
            GetCars();
        }

        public async void OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<CarModel> args)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", args.Data.Id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public void GetCars()
        {
            cars = CarService.GetCars().ToList();
        }
    }
}
