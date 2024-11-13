using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Radzen;
using System.Text.Json;
using Microsoft.AspNetCore.StaticFiles;

namespace MecuryProduct.Modals
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

        /// <summary>Injects various services into the current class.</summary>
        /// <remarks>
        /// The services injected are:
        /// - CarService: Service for managing cars.
        /// - DriverService: Service for managing drivers.
        /// - AuthenticationStateProvider: Provider for authentication state.
        /// - DialogService: Service for displaying dialogs.
        /// - SessionService: Service for managing sessions.
        /// - DocService: Service for handling documents.
        /// </remarks>
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
        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private CustomerService CustomerService { get; set; }

        /// <summary>
        /// Initializes the page by retrieving necessary data and setting up the session.
        /// </summary>
        /// <remarks>
        /// This method performs the following tasks:
        /// 1. Retrieves a list of drivers.
        /// 2. Sets the user ID.
        /// 3. Retrieves a list of car makes.
        /// 4. Retrieves a list of car years.
        /// 5. Retrieves a CarModel object from the session named "car_form".
        /// 6. Retrieves a DocModel object from the session named "doc".
        /// 7. Retrieves a DocModel object from the session named "update_doc".
        /// 8. Retrieves a list of DocModel objects from the session named "vehicle_images".
        /// 9. Updates the
        protected override async void OnInitialized()
        {
            GetDriversByUserId();
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

        public async void GetDriversByUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    var role = DriverService.GetUserClaimByUserId(userId);

                    if (role == "Manager")
                    {
                        var company = await SessionService.Get<int>("company");
                        drivers = DriverService.GetUsersByClaimByCompanyId("Role", "Driver", company);
                    }
                    else if (role == "Employee")
                    {
                        var userById = DriverService.GetUserById(userId);
                        //drivers = DriverService.GetUsersByClaimByCompanyId("Role", "Driver", userById.CompanyId);
                    }
                    else
                    {
                        GetDrivers();
                    }
                }
            }
        }

        /// <summary>
        /// Opens a modal dialog to add a vehicle for a specific customer.
        /// </summary>
        /// <param name="CusId">The ID of the customer for whom the vehicle is being added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenAddVehicleModal(int CusId)
        {
            await DialogService.OpenAsync<AddVehicleModal>("Add Vehicle",
                new Dictionary<string, object>() { { "CusId", CusId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>
        /// Retrieves the list of car makes from the CarService and assigns it to the 'makes' variable.
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
        /// Changes the make of a car and updates the available models accordingly.
        /// </summary>
        /// <param name="make">The new make of the car.</param>
        /// <remarks>
        /// This method sets the car's make, clears the car model, and retrieves the available models based on the new make.
        /// </remarks>
        public void ChangeMake(string? make)
        {
            SetInSession();

            // PP-95: when make change model should remove
            // Bug: Whenever the make changes model should remove
            // Fix: Assign empty string in model when make changes
            car.car_model = string.Empty;

            models = CarService.GetModelsByMake(make);
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
        /// Creates a new car entry with associated documents and images.
        /// </summary>
        /// <remarks>
        /// The method sets the creation and update timestamps for the car, adds the car with notes using the CarService.
        /// If there are documents associated with the car, they are linked and added using the DocService.
        /// Any existing documents to update are also linked and added.
        /// Images related to the car are associated and added as documents.
        /// The method clears the session data for the car form.
        /// A confirmation dialog is displayed to inquire about adding another vehicle.
        /// If confirmed, a new vehicle modal is opened for the specified customer ID.
        /// </remarks>
        public async void CreateCar()
        {
            car.CompanyId = CustomerService.GetCustomerById(CusID)?.CompanyId;
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
            // PP-60: after adding a vehicle, show a confirmation modal that says "add another vehicle" and "done"
            // Feature: It should ask for adding second vehicle or not after adding first vehicle
            // Fix: I have added a confirmation modal after commiting or saving the vehicle
            bool? addAnotherVehicle = await DialogService.Confirm("Are you sure?", "Do you want to add another vehicle?", new ConfirmOptions() { OkButtonText = "Add Another Vehicle", CancelButtonText = "Done" });
            if (addAnotherVehicle != null && addAnotherVehicle == true)
            {
                dialogService.Close(true);
                await OpenAddVehicleModal(CusID);
            }
            else
            {
                dialogService.Close(true);
            }
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

        /// <summary>Checks if a date should be disabled for rendering.</summary>
        /// <param name="args">The DateRenderEventArgs containing information about the date to render.</param>
        void DateRender(DateRenderEventArgs args)
        {
            args.Disabled = args.Disabled || args.Date.DayOfWeek == DayOfWeek.Sunday || args.Date.Date < DateTime.Today;
        }

        /// <summary>
        /// Sets the user ID for the current user.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state of the user and sets the user ID if the user is authenticated.
        /// </remarks>
        /// <returns>Void</returns>
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

        /// <summary>
        /// Changes the VIN image for a car based on the provided base64 string.
        /// </summary>
        /// <param name="base64">The base64 string representing the image to be saved.</param>
        /// <remarks>
        /// If the base64 string is not null, the method locates the VIN document for the car, updates its information, and saves the image file.
        /// If the VIN document does not exist, a new document is created and saved.
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
        /// Changes the vehicle images based on the provided upload change event arguments.
        /// </summary>
        /// <param name="e">The Radzen.UploadChangeEventArgs containing information about the uploaded files.</param>
        /// <remarks>
        /// This method iterates through the uploaded files, saves them to the specified directory, and updates the vehicle images.
        /// </remarks>
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
