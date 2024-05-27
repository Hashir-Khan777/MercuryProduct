using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Radzen;
using System.Text.Json;

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
        private List<string> makes = new List<string>();
        private List<string> models = new List<string>();
        private List<int?> years = new List<int?>();

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        protected override async void OnInitialized()
        {
            GetDrivers();
            SetUserId();
            GetMakes();
            GetYears();

            var result = await SessionService.Get<CarModel>("car_form");

            if (result != null)
            {
                car = result;
                models = CarService.GetModelsByMake(car.car_make);
            }
        }

        public async Task OpenAddVehicleModal(int CusId)
        {
            await DialogService.OpenAsync<AddVehicleModal>("Add Vehicle",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        public void GetMakes()
        {
            makes = CarService.GetMakes();
        }

        public void GetYears()
        {
            years = CarService.GetYear();
        }

        public void ChangeMake(string? make)
        {
            SetInSession();

            models = CarService.GetModelsByMake(make);
        }

        public async void SetInSession()
        {
            await SessionService.Set("car_form", JsonSerializer.Serialize(car));
        }

        public async void CreateCar()
        {
            car.created_at = DateTime.UtcNow;
            car.updated_at = DateTime.UtcNow;
            CarService.AddCar(car, veh_notes);
            bool? addAnotherVehicle = await DialogService.Confirm("Are you sure?", "Do you want to add another vehicle?", new ConfirmOptions() { OkButtonText = "Add Another Vehicle", CancelButtonText = "Done" });
            if (addAnotherVehicle != null && addAnotherVehicle == true)
            {
                await OpenAddVehicleModal(CusID);
            }
            else
            {
                dialogService.Close(true);
            }
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
