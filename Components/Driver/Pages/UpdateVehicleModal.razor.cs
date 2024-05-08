using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Driver.Pages
{
    public partial class UpdateVehicleModal
    {
        [Parameter]
        public int id { get; set; }
        private CarModel car = new CarModel();
        private List<string> statuses = new List<string>()
        {
            "Scheduled",
            "Bought",
            "Builder",
            "DnD"
        };
        private List<string> title_status = new List<string>()
        {
            "Yes",
            "No",
        };
        private List<string> tires_condition = new List<string>()
        {
            "Good",
            "Flat",
            "M Left",
            "M Right",
            "M Both",
        };
        private List<string> motor_condition = new List<string>()
        {
            "Running",
            "Bad Motor",
            "Missing Motor",
        };
        private List<string> colors = new List<string>()
        {
            "Black",
            "Blue",
            "Brown",
            "Burgundy",
            "Camo",
            "Gold",
            "Greay",
            "Red",
            "White",
            "Green",
            "Silver",
            "Yellow",
            "Other"
        };
        private List<string> pull_type = new List<string>()
        {
            "Short",
            "Long"
        };

        [Inject]
        private CarService CarService { get; set; }

        protected override void OnInitialized()
        {
            var getCarById = CarService.GetCarById(id);
            if (getCarById != null)
            {
                car = getCarById;
            }
        }

        public void UpdateCar()
        {
            car.updated_at = DateTime.UtcNow;
            CarService.UpdateCar(car);
            dialogService.Close();
        }
    }
}
