using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Modals
{
    public partial class UpdateVehicleModal
    {
        [Parameter] public int VehId { get; set; }
        [Parameter] public bool Inventory { get; set; } = false;
        [Parameter] public bool Production { get; set; } = false;
        private CarModel car = new CarModel();
        private List<ApplicationUser> drivers = new List<ApplicationUser>();
        private List<CustomerModel> customers = new List<CustomerModel>();
        public List<DocModel> docs = new List<DocModel>();
        public List<CompanyModel> companies = new List<CompanyModel>();
        private List<string> statuses = new List<string>()
        {
            "Scheduled",
            "Picked Up",
            "Follow Up",
            "Bought",
            "DnD"
        };
        private List<string> driver_statuses = new List<string>()
        {
            "Bought",
            "DnD"
        };
        private List<Instruction> title_status = new List<Instruction>()
        {
            new Instruction { label = "Yes", value = true },
            new Instruction { label = "No", value = false },
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
        private List<string> prod_status = new List<string>()
        {
            "Hold",
            "Set",
            "Pulled",
        };
        private List<Instruction> special_instructions = new List<Instruction>()
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
        private List<string> sections = new List<string>();
        private List<string> rows = new List<string>();
        private List<string> makes = new List<string>();
        private List<string> models = new List<string>();
        private List<int?> years = new List<int?>();
        public string? veh_notes = null;
        public DocModel doc;
        private string user_role = string.Empty;

        /// <summary>Injects various services into the class.</summary>
        /// <remarks>
        /// The services injected are:
        /// - CarService: Service for managing cars.
        /// - UserService: Service for managing users.
        /// - CustomerService: Service for managing customers.
        /// - DocService: Service for managing documents.
        /// - ProductionService: Service for managing production.
        /// - NoteService: Service for managing notes.
        /// </remarks>
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private UserService DriverService { get; set; }
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private DocService DocService { get; set; }
        [Inject]
        private ProductionService ProductionService { get; set; }
        [Inject]
        private NoteService NoteService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private ExpenseService ExpenseService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// Initializes the component by retrieving necessary data such as drivers, customers, cars, sections, makes, and years.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCarById();
            GetAllSections();
            GetMakes();
            GetYears();
            SetUserId();
        }

        /// <summary>
        /// Updates the car information based on its production status and additional notes.
        /// </summary>
        /// <remarks>
        /// If the production status is "Set", the set date is updated to the current date and time.
        /// If the production status is "Pulled", the pulled date is updated to the current date and time.
        /// If there are vehicle notes and a document is provided, a new note is added to the database.
        /// The car's updated date is set to the current date and time.
        /// Finally, the car information is updated in the database and the dialog is closed.
        /// </remarks>
        public void UpdateCar()
        {
            if (car.prod_status == "Set")
            {
                car.set_date = DateTime.UtcNow;
            }
            if (car.prod_status == "Pulled")
            {
                car.pulled_date = DateTime.UtcNow;
            }
            if (veh_notes is not null && doc != null)
            {
                NoteService.AddNote(new NoteModel { doc_id = doc.Id, note = veh_notes, created_by_id = car.created_by_id, created_at = DateTime.UtcNow, updated_at = DateTime.UtcNow });
            }
            car.updated_at = DateTime.UtcNow;
            CarService.UpdateCar(car);
            if (car.status == "Bought")
            {
                ExpenseModel expense = new ExpenseModel
                {
                    type = "car_buy",
                    cus_id = (int)car.cid,
                    company_id = (int)car.CompanyId,
                    created_by_id = car.created_by_id,
                    payment_type = "cash",
                    amount = (double)car.purchase_amount,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow,
                };
                ExpenseService.AddExpense(expense);
            }
            dialogService.Close();
        }

        /// <summary>
        /// Retrieves car information by ID and sets related properties.
        /// </summary>
        /// <remarks>
        /// This method fetches car details by ID using the CarService, and then populates various properties such as models, rows, docs, vehicleImages, and vinImage based on the retrieved car information.
        /// </remarks>
        public void GetCarById()
        {
            var getCarById = CarService.GetCarById(VehId);
            if (getCarById != null)
            {
                car = getCarById;
                models = CarService.GetModelsByMake(car.car_make).ToList();
                rows = ProductionService.GetRowsBySection(car.section).ToList();
                var vehicle_docs = getCarById.docs?.FindAll(d => d.type.ToLower() == "doc");
                if (vehicle_docs is not null)
                {
                    docs = vehicle_docs;
                }
                var vin = car.docs?.Find(d => d.type.ToLower() == "vin");
                var vehicles = car.docs?.FindAll(d => d.type.ToLower() == "vehicle");
                if (vehicles is not null)
                {
                    vehicleImages = vehicles;
                }
                if (vin is not null)
                {
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(vin.file_path, out string contentType))
                    {
                        contentType = "application/octet-stream";
                    }
                    file_name = vin.file_name;
                    byte[] imageArray = File.ReadAllBytes(vin.file_path);
                    vinImage = $"data:{contentType};base64,{Convert.ToBase64String(imageArray)}";
                }
            }
        }

        /// <summary>
        /// Sets the user ID based on the authenticated user's information.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state of the user and sets the user ID if the user is authenticated.
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
                    user_role = UserService.GetUserClaimByUserId(userId);
                    var company = await SessionService.Get<int>("company");

                    if (user_role == "Manager")
                    {
                        companies = CompanyService.GetCompaniesByManagerId(userId);
                        drivers = DriverService.GetUsersByClaimByCompanyId("Role", "Driver", company);
                        customers = CustomerService.GetCustomersByCompanyId(company);
                    }
                    else if (user_role == "Employee")
                    {
                        companies = CompanyService.GetCompaniesByEmployeeId(userId);
                        drivers = DriverService.GetUsersByClaimByCompanyId("Role", "Driver", company);
                        customers = CustomerService.GetCustomersByCompanyId(company);
                    }
                    else
                    {
                        companies = CompanyService.GetCompanies();
                        GetDrivers();
                        GetCustomers();
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a document from the system.
        /// </summary>
        /// <param name="doc">The document to be deleted.</param>
        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            docs.Remove(doc);
            vehicleImages.Remove(doc);
            StateHasChanged();
        }

        /// <summary>
        /// Retrieves a list of car makes from the CarService and assigns it to the 'makes' variable.
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
        /// Changes the make of the car and updates the available models based on the new make.
        /// </summary>
        /// <param name="make">The new make of the car.</param>
        public void ChangeMake(string? make)
        {
            car.car_model = string.Empty;
            models = CarService.GetModelsByMake(make);
        }

        /// <summary>
        /// Changes the VIN image for a car based on the provided base64 string.
        /// </summary>
        /// <param name="base64">The base64 string representing the image to be saved.</param>
        /// <remarks>
        /// If the base64 string is not null, the method locates the VIN document for the car, updates its information, and saves the image file.
        /// If the VIN document does not exist, a new document is created and saved with the provided image.
        /// </remarks>
        public void changeVinImage(string? base64)
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
                    vin.veh_id = car.Id;
                    vin.short_path = "uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}";
                    vin.updated_at = DateTime.UtcNow;
                    int startingIndex = base64.IndexOf(";base64,") + 8;
                    string fileBase64 = base64.Substring(startingIndex);
                    byte[] file = Convert.FromBase64String(fileBase64);
                    System.IO.File.WriteAllBytes(filePath, file);
                    DocService.UpdateDoc(vin);
                }
                else
                {
                    var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                    string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}";
                    DocModel doc = new DocModel()
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
                    DocService.AddDoc(doc);
                }
            }
        }

        /// <summary>
        /// Changes the vehicle images based on the provided upload change event arguments.
        /// </summary>
        /// <param name="e">The Radzen.UploadChangeEventArgs containing information about the uploaded files.</param>
        /// <remarks>
        /// This method iterates through the uploaded files, saves them to the specified directory,
        /// and updates the vehicle images collection and database records accordingly.
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
                    veh_id = car.Id,
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
                DocService.AddDoc(doc);
                StateHasChanged();
            }
        }

        /// <summary>
        /// Retrieves all sections from the production service and stores them in a list.
        /// </summary>
        /// <remarks>
        /// This method fetches all sections from the production service and converts them to a list for further processing.
        /// </remarks>
        public void GetAllSections()
        {
            sections = ProductionService.GetAllSections().ToList();
        }

        /// <summary>
        /// Retrieves all rows by a specified section and assigns them to the 'rows' variable.
        /// </summary>
        /// <param name="section">The section to retrieve rows from.</param>
        public void GetAllRowsBySection(string section)
        {
            car.row = string.Empty;
            rows = ProductionService.GetRowsBySection(section).ToList();
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
        /// Retrieves a list of customers from the CustomerService and assigns it to the 'customers' field.
        /// </summary>
        public async void GetCustomers()
        {
            var company = await SessionService.Get<int>("company");
            customers = CustomerService.GetCustomersByCompanyId(company);
        }

        /// <summary>Checks if a date should be disabled for rendering.</summary>
        /// <param name="args">The DateRenderEventArgs containing information about the date to render.</param>
        void DateRender(DateRenderEventArgs args)
        {
            args.Disabled = args.Disabled || args.Date.DayOfWeek == DayOfWeek.Sunday || args.Date.Date < DateTime.Today;
        }

        /// <summary>
        /// Changes the documents based on the provided upload change event arguments.
        /// </summary>
        /// <param name="e">The upload change event arguments containing information about the files.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"stk-{car.Id}-doc-{datetime}-{file.Name}";
                doc = new DocModel()
                {
                    file_name = file.Name,
                    file_path = filePath,
                    type = "doc",
                    server_name = "localhost",
                    veh_id = car.Id,
                    short_path = "uploads/" + $"stk-{car.Id}-doc-{datetime}-{file.Name}",
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
                DocService.AddDoc(doc);
                docs = [doc];
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
