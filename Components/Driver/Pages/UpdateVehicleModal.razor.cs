using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.StaticFiles;

namespace MecuryProduct.Components.Driver.Pages
{
    public partial class UpdateVehicleModal
    {
        [Parameter]
        public int id { get; set; }
        private CarModel car = new CarModel();
        // PP-93: remove delivered status in driver view
        // Bug: Delivered is not the valid status should change to Bought
        // Fix: Change Delivered to Bought
        private List<string> statuses = new List<string>()
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
        private string file_name = string.Empty;
        private List<string> makes = new List<string>();
        private List<string> models = new List<string>();
        private List<int?> years = new List<int?>();
        public string vinImage;
        public List<DocModel> vehicleImages = new List<DocModel>();

        /// <summary>Injects the CarService and DocService dependencies.</summary>
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DocService DocService { get; set; }

        /// <summary>
        /// Initializes the view model by retrieving necessary data such as car details, models, and images.
        /// </summary>
        /// <remarks>
        /// This method populates the view model with data required for display.
        /// It retrieves car details by ID, including associated models and images.
        /// </remarks>
        protected override void OnInitialized()
        {
            GetMakes();
            GetYears();
            var getCarById = CarService.GetCarById(id);
            if (getCarById != null)
            {
                car = getCarById;
                models = CarService.GetModelsByMake(car.car_make);
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
        /// Updates the list of car models based on the specified make.
        /// </summary>
        /// <param name="make">The make of the car for which models need to be retrieved.</param>
        public void ChangeMake(string? make)
        {
            models = CarService.GetModelsByMake(make);
        }

        /// <summary>
        /// Updates the car information including pickup date, timestamp, and other details.
        /// </summary>
        /// <remarks>
        /// If the car status is "bought", the pickup date is set to the current UTC date and time.
        /// The car's updated timestamp is set to the current UTC date and time.
        /// The VIN number and DL (Driver's License) are converted to uppercase.
        /// If there are vehicle images associated with the car, the car information is updated using the CarService
        /// and the dialog service is closed.
        /// </remarks>
        public void UpdateCar()
        {
            if (car.status.ToLower() == "bought")
            {
                car.pickup_date = DateTime.UtcNow;
            }
            // PP-68: When driver add VIN it should covert to caps
            // Feature: Vin should be capital
            // Fix: Add ToUpper function on before updating vehicle on vin and dl
            car.updated_at = DateTime.UtcNow;
            car.vin_no = car.vin_no.ToUpper();
            car.DL = car.DL.ToUpper();
            if (vehicleImages.Count() > 0)
            {
                CarService.UpdateCar(car);
                dialogService.Close();
            }
        }

        /// <summary>
        /// Changes the VIN image for a car based on the provided base64 string.
        /// </summary>
        /// <param name="base64">The base64 string representing the image to be saved.</param>
        /// <remarks>
        /// If the base64 string is not null, the method locates the VIN document for the car, updates its information, and saves the image file.
        /// If the VIN document does not exist, a new document is created and saved with the provided image.
        /// </remarks>
        // PP-69: change image name to stock id
        // Bug: image name should be unique (to prevent overiding issue)
        // Fix: Add a stock number and date to the image name
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
        /// Deletes a document from the system.
        /// </summary>
        /// <param name="doc">The document to be deleted.</param>
        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            vehicleImages.Remove(doc);
            StateHasChanged();
        }

        /// <summary>
        /// Changes the vehicle images based on the provided upload change event arguments.
        /// </summary>
        /// <param name="e">The Radzen.UploadChangeEventArgs containing information about the changed files.</param>
        /// <remarks>
        /// This method iterates through the files in the upload change event arguments, saves them to the specified directory,
        /// and updates the vehicle images accordingly.
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
        /// Represents an instruction with a label and a boolean value.
        /// </summary>
        private sealed class Instruction
        {
            public string label { get; set; } = string.Empty;
            public bool value { get; set; }
        }
    }
}
