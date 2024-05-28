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
        private List<string> statuses = new List<string>()
        {
            "Bought",
            "Delivered",
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

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DocService DocService { get; set; }

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
            models = CarService.GetModelsByMake(make);
        }

        public void UpdateCar()
        {
            if (car.status.ToLower() == "bought")
            {
                car.pickup_date = DateTime.UtcNow;
            }
            car.updated_at = DateTime.UtcNow;
            car.vin_no = car.vin_no.ToUpper();
            car.DL = car.DL.ToUpper();
            if (vehicleImages.Count() > 0)
            {
                CarService.UpdateCar(car);
                dialogService.Close();
            }
        }

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
                    DocModel doc = new DocModel()
                    {
                        Id = vin.Id,
                        file_name = file_name,
                        file_path = filePath,
                        type = "vin",
                        server_name = "localhost",
                        veh_id = car.Id,
                        short_path = "uploads/" + $"stk-{car.Id}-vin-{datetime}-{file_name}",
                        updated_at = DateTime.UtcNow,
                        created_at = vin.created_at,
                    };
                    int startingIndex = base64.IndexOf(";base64,") + 8;
                    string fileBase64 = base64.Substring(startingIndex);
                    byte[] file = Convert.FromBase64String(fileBase64);
                    System.IO.File.WriteAllBytes(filePath, file);
                    DocService.UpdateDoc(doc);
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
                        short_path = "uploads/" + $"stk-{ car.Id }-vin-{datetime}-{ file_name }",
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

        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
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

        private sealed class Instruction
        {
            public string label { get; set; } = string.Empty;
            public bool value { get; set; }
        }
    }
}
