using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddVehicleModal
    {
        private CarModel car = new CarModel();
        [Parameter] public int CusID { get; set; }
        private List<ApplicationUser> drivers = new List<ApplicationUser>();
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
        private string veh_notes = "";

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            GetDrivers();
            SetUserId();
        }

        public void CreateCar()
        {
            car.created_at = DateTime.UtcNow;
            car.updated_at = DateTime.UtcNow;
            CarService.AddCar(car, veh_notes);
            dialogService.Close(true);
        }

        public void GetDrivers()
        {
            drivers = DriverService.GetUsersByClaim("Role", "Driver");
        }

        void DateRender(DateRenderEventArgs args)
        {
            args.Disabled = args.Disabled || args.Date.DayOfWeek == DayOfWeek.Sunday || args.Date.Date < DateTime.Today;
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    car.created_by_id = userId;
                    car.cid = CusID;
                }
            }
        }

        private sealed class Instruction
        {
            public string label { get; set; } = string.Empty;
            public bool value { get; set; }
        }
    }
}
