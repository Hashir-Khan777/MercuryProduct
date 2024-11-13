using MecuryProduct.Data;
using MecuryProduct.Modals;
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
        public List<CompanyModel> companies = new List<CompanyModel>();
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

        /// <summary>
        /// Represents a class that manages dependency injection for various services and components.
        /// </summary>
        /// <remarks>
        /// This class provides properties to access different services using dependency injection.
        /// </remarks>
        [Inject]
        private CarService CarService { get; set; }
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
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        /// <summary>
        /// Initializes the view model by retrieving necessary data from session and services.
        /// </summary>
        /// <remarks>
        /// This method performs the following tasks:
        /// 1. Retrieves drivers and customers information.
        /// 2. Sets the user ID.
        /// 3. Retrieves car makes and years.
        /// 4. Retrieves session data for car form, document, updated document, and vehicle images.
        /// 5. Processes the retrieved session data to update the view model properties.
        /// </remarks>
        protected override async void OnInitialized()
        {
            GetDrivers();
            GetCustomers();
            SetUserId();
            GetMakes();
            GetYears();

            companies = CompanyService.GetCompanies();

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

        public async void OpenAddCustomerModal()
        {
            var result = await DialogService.OpenAsync<AddCustomerModal>("Add Customer",
                new Dictionary<string, object>() { },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            GetCustomers();
            car.cid = result;
            StateHasChanged();
        }

        /// <summary>
        /// Creates a new car entity with associated documents and images.
        /// </summary>
        /// <remarks>
        /// This method sets the creation and update timestamps for the car entity, adds the car to the CarService,
        /// associates any provided document with the car, associates any provided updated document with the car,
        /// associates any provided images with the car, clears the session data related to the car form,
        /// and navigates to the admin vehicles page.
        /// </remarks>
        public async void CreateCar()
        {
            car.created_at = DateTime.UtcNow;
            car.updated_at = DateTime.UtcNow;
            car.CompanyId = await SessionService.Get<int>("company");
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

        /// <summary>
        /// Sets the car form data in the session.
        /// </summary>
        /// <remarks>
        /// This method serializes the car object using JSON and stores it in the session with the key "car_form".
        /// </remarks>
        public async void SetInSession()
        {
            await SessionService.Set("car_form", JsonSerializer.Serialize(car));
        }

        /// <summary>
        /// Retrieves the list of car makes from the CarService.
        /// </summary>
        public void GetMakes()
        {
            makes = CarService.GetMakes();
        }

        /// <summary>
        /// Retrieves the years of available cars from the CarService.
        /// </summary>
        public void GetYears()
        {
            years = CarService.GetYear();
        }

        /// <summary>
        /// Changes the make of the car and updates the available models accordingly.
        /// </summary>
        /// <param name="make">The new make of the car.</param>
        /// <remarks>
        /// This method sets the car's make, clears the car model, and retrieves the available models based on the new make.
        /// </remarks>
        public void ChangeMake(string? make)
        {
            SetInSession();

            car.car_model = string.Empty;

            models = CarService.GetModelsByMake(make);
        }

        /// <summary>
        /// Retrieves a list of drivers from the DriverService based on a specific claim.
        /// </summary>
        /// <remarks>
        /// This method populates the 'drivers' field with a list of users who have the specified claim.
        /// </remarks>
        public async void GetDrivers()
        {
            var company = await SessionService.Get<int>("company");
            drivers = DriverService.GetUsersByClaimByCompanyId("Role", "Driver", company);
        }

        /// <summary>
        /// Retrieves a list of customers from the CustomerService and assigns it to the customers field.
        /// </summary>
        public async void GetCustomers()
        {
            var company = await SessionService.Get<int>("company");
            customers = CustomerService.GetCustomersByCompanyId(company);
        }

        /// <summary>
        /// Sets the user ID based on the authenticated user's information.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state of the user and extracts the user ID from the claims.
        /// If the user is authenticated and the user ID is found in the claims, it sets the created_by_id property to the user ID.
        /// </remarks>
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

        /// <summary>
        /// Changes the VIN image for a car based on the provided base64 string.
        /// </summary>
        /// <param name="base64">The base64 string representing the image to be saved.</param>
        /// <remarks>
        /// If the base64 string is not null, the VIN image for the car is updated or added based on the existing VIN document.
        /// The image is saved in the specified directory with a unique file name and path.
        /// </remarks>
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

        /// <summary>Deletes a document from the list of vehicle images.</summary>
        /// <param name="doc">The document to be deleted.</param>
        public void DeleteDoc(DocModel doc)
        {
            vehicleImages.Remove(doc);
            StateHasChanged();
        }

        /// <summary>
        /// Changes and uploads vehicle images to the server.
        /// </summary>
        /// <param name="e">The event arguments containing the uploaded files.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
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

        /// <summary>Checks if a date should be disabled for rendering.</summary>
        /// <param name="args">The DateRenderEventArgs containing information about the date to render.</param>
        void DateRender(DateRenderEventArgs args)
        {
            args.Disabled = args.Disabled || args.Date.DayOfWeek == DayOfWeek.Sunday || args.Date.Date < DateTime.Today;
        }

        /// <summary>
        /// Represents an instruction with a label and a boolean value.
        /// </summary>
        private sealed class Instruction
        {
            public string label { get; set; } = string.Empty;
            public bool value { get; set; }
        }
    }
}
