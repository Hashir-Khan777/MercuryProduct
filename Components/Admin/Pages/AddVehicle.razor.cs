using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Radzen;
using System.Security.Claims;
using System.Text.Json;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddVehicle
    {
        private CarModel car = new CarModel();
        private List<ApplicationUser> drivers = new List<ApplicationUser>();
        private List<CustomerModel> customers = new List<CustomerModel>();
        private List<string> statuses = new List<string>()
        {
            "Scheduled",
            "Picked Up",
            "Follow Up",
            "Bought",
            "DnD"
        };
        private List<Instruction> title_status = new List<Instruction>()
        {
            new Instruction { label = "Yes", value = true },
            new Instruction { label = "No", value = false },
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
        private string file_name = string.Empty;
        public string vinImage;
        public List<DocModel> vehicleImages = new List<DocModel>();
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
        private DocModel? doc;
        private DocModel? update_doc;

        [Inject]
        private CarService CarService {  get; set; }
        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private DocService DocService { get; set; }

        protected override async void OnInitialized()
        {
            GetDrivers();
            GetCustomers();
            SetUserId();
            GetMakes();
            GetYears();

            var result = await SessionService.Get<CarModel>("car_form");
            var session_doc = await SessionService.Get<DocModel>("doc");
            update_doc = await SessionService.Get<DocModel>("update_doc");
            var session_vehicleImages = await SessionService.Get<List<DocModel>>("vehicle_images");

            if (session_vehicleImages is not null)
            {
                vehicleImages = session_vehicleImages;
            }
            if (session_doc is not null)
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(session_doc.file_path, out string contentType))
                {
                    contentType = "application/octet-stream";
                }
                file_name = session_doc.file_name;
                byte[] imageArray = File.ReadAllBytes(session_doc.file_path);
                vinImage = $"data:{contentType};base64,{Convert.ToBase64String(imageArray)}";
            }
            if (result != null)
            {
                car = result;
                models = CarService.GetModelsByMake(car.car_make);
            }
        }

        public async void CreateCar()
        {
            car.created_at = DateTime.UtcNow;
            car.updated_at = DateTime.UtcNow;
            CarService.AddCar(car, veh_notes);
            if (doc is not null)
            {
                doc.veh_id = car.Id;
                DocService.AddDoc(doc);
                doc = null;
            }
            if (update_doc is not null)
            {
                update_doc.veh_id = car.Id;
                DocService.AddDoc(update_doc);
                update_doc = null;
            }
            if (vehicleImages.Count() > 0)
            {
                foreach (var item in vehicleImages)
                {
                    item.veh_id = car.Id;
                    DocService.AddDoc(item);
                }
                vehicleImages = new List<DocModel>();
            }
            await SessionService.Clear("car_form");
            NavigationManager.NavigateTo("/admin/vehicles");
        }

        public async void SetInSession()
        {
            await SessionService.Set("car_form", JsonSerializer.Serialize(car));
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

            car.car_model = string.Empty;

            models = CarService.GetModelsByMake(make);
        }

        public void GetDrivers()
        {
            drivers = DriverService.GetUsersByClaim("Role", "Driver");
        }

        public void GetCustomers()
        {
            customers = CustomerService.GetCustomers();
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
                }
            }
        }

        public async void changeVinImage(string? base64)
        {
            if (base64 is not null)
            {
                var vin = car.docs?.Find(d => d.type.ToLower() == "vin");
                string directory = Directory.GetCurrentDirectory();
                if (vin is not null)
                {
                    var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                    string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}";
                    vin.file_name = file_name;
                    vin.file_path = filePath;
                    vin.short_path = "uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}";
                    vin.updated_at = DateTime.UtcNow;
                    int startingIndex = base64.IndexOf(";base64,") + 8;
                    string fileBase64 = base64.Substring(startingIndex);
                    byte[] file = Convert.FromBase64String(fileBase64);
                    System.IO.File.WriteAllBytes(filePath, file);
                    update_doc = vin;
                    await SessionService.Set("update_doc", JsonSerializer.Serialize(update_doc));
                }
                else
                {
                    var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                    string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}";
                    doc = new DocModel()
                    {
                        file_name = file_name,
                        file_path = filePath,
                        type = "vin",
                        server_name = "localhost",
                        veh_id = car.Id,
                        short_path = "uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}",
                        created_at = DateTime.UtcNow,
                        updated_at = DateTime.UtcNow
                    };
                    int startingIndex = base64.IndexOf(";base64,") + 8;
                    string fileBase64 = base64.Substring(startingIndex);
                    byte[] file = Convert.FromBase64String(fileBase64);
                    System.IO.File.WriteAllBytes(filePath, file);
                    await SessionService.Set("doc", JsonSerializer.Serialize(doc));
                }
            }
        }

        public void DeleteDoc(DocModel doc)
        {
            vehicleImages.Remove(doc);
            StateHasChanged();
        }

        public async void changeVehicleImages(Radzen.UploadChangeEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{car.Id}-vehicle-{datetime}-{file.Name}";
                DocModel doc = new DocModel()
                {
                    file_name = file.Name,
                    file_path = filePath,
                    type = "vehicle",
                    server_name = "localhost",
                    short_path = "uploads/" + $"stk-{car.Id}-vehicle-{datetime}-{file.Name}",
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                await using (var stream = file.OpenReadStream(long.MaxValue))
                {
                    await using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
                vehicleImages.Add(doc);
                await SessionService.Set("vehicle_images", JsonSerializer.Serialize(vehicleImages));
                StateHasChanged();
            }
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
