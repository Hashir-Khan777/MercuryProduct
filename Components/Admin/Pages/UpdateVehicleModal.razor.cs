using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class UpdateVehicleModal
    {
        [Parameter] public int VehId {  get; set; }
        private CarModel car = new CarModel();
        private List<ApplicationUser> drivers = new List<ApplicationUser>();
        private List<CustomerModel> customers = new List<CustomerModel>();
        private List<string> statuses = new List<string>()
        {
            "Scheduled",
            "Picked Up",
            "Follow Up",
            "Delivered",
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
        private List<string> vehicle_type = new List<string>()
        {
            "Builder",
            "SP",
            "Yard",
            "Full Service",
        };
        private List<string> motor_condition = new List<string>()
        {
            "Running",
            "Bad Motor",
            "Missing Motor",
        };
        private List<Instruction> special_instructions = new List<Instruction>()
        {
            new Instruction { label = "Yes", value = true },
            new Instruction { label = "No", value = false },
        };

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private CustomerService CustomerService { get; set; }

        protected override void OnInitialized()
        {
            GetDrivers();
            GetCustomers();
            GetCarById();
        }

        public void UpdateCar()
        {
            car.updated_at = DateTime.UtcNow;
            CarService.UpdateCar(car);
            dialogService.Close();
        }

        public void GetCarById()
        {
            var getCarById = CarService.GetCarById(VehId);
            if (getCarById != null)
            {
                car = getCarById;
            }
        }

        public void GetDrivers()
        {
            drivers = DriverService.GetUsersByClaim("Role", "Driver");
        }

        public void GetCustomers()
        {
            customers = CustomerService.GetCustomers();
        }

        void DateRender(DateRenderEventArgs args)
        {
            args.Disabled = args.Disabled || args.Date.DayOfWeek == DayOfWeek.Sunday || args.Date.Date < DateTime.Today;
        }

        private sealed class Instruction
        {
            public string label { get; set; } = string.Empty;
            public bool value { get; set; }
        }
    }
}
