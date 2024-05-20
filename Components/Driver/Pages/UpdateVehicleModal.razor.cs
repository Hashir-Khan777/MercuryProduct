using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using Radzen.Blazor;
using System.Text;

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
        private string file_name = string.Empty;

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private ImageService ImageService { get; set; }

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

        public void changeVinImage(string base64)
        {
            string filePath = @"E:\Zini Tecnologies Projects\MecuryProduct\wwwroot\uploads\" + file_name;
            ImageModel image = new ImageModel()
            {
                file_name = file_name,
                file_path = filePath,
                type = "vin",
                server_name = "localhost",
                veh_id = car.Id
            };
            int startingIndex = base64.IndexOf(";base64,") + 8;
            string fileBase64 = base64.Substring(startingIndex);
            byte[] file = Convert.FromBase64String(fileBase64);
            System.IO.File.WriteAllBytes(filePath, file);
            ImageService.AddImage(image);
        }

        public async void changeVehicleImages(Radzen.UploadChangeEventArgs e)
        {
            foreach (var file in e.Files)
            {
                string filePath = @"E:\Zini Tecnologies Projects\MecuryProduct\wwwroot\uploads\" + file.Name;
                ImageModel image = new ImageModel()
                {
                    file_name = file.Name,
                    file_path = filePath,
                    type = "vehicle",
                    server_name = "localhost",
                    veh_id = car.Id
                };
                await using (var stream = file.OpenReadStream(long.MaxValue))
                {
                    await using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await stream.CopyToAsync(fs);
                    }
                }
                ImageService.AddImage(image);
            }
        }
    }
}
