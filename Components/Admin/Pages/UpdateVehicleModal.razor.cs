using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class UpdateVehicleModal
    {
        [Parameter] public int VehId {  get; set; }
        [Parameter] public bool Inventory { get; set; } = false;
        [Parameter] public bool Production { get; set; } = false;
        private CarModel car = new CarModel();
        private List<ApplicationUser> drivers = new List<ApplicationUser>();
        private List<CustomerModel> customers = new List<CustomerModel>();
        public List<DocModel> docs = new List<DocModel>();
        private List<string> statuses = new List<string>()
        {
            "Scheduled",
            "Picked Up",
            "Follow Up",
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
        private List<string> sections = new List<string>();
        private List<string> rows = new List<string>();
        private List<string> makes = new List<string>();
        private List<string> models = new List<string>();
        private List<int?> years = new List<int?>();
        public string? veh_notes = null;
        public DocModel doc;

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

        protected override void OnInitialized()
        {
            GetDrivers();
            GetCustomers();
            GetCarById();
            GetAllSections();
            GetMakes();
            GetYears();
        }

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
            dialogService.Close();
        }

        public void GetCarById()
        {
            var getCarById = CarService.GetCarById(VehId);
            if (getCarById != null)
            {
                car = getCarById;
                models = CarService.GetModelsByMake(car.car_make);
                GetAllRowsBySection(car.section);
                var vehicle_docs = getCarById.docs?.FindAll(d => d.type.ToLower() == "doc");
                if (vehicle_docs is not null)
                {
                    docs = vehicle_docs;
                }
            }
        }

        public void DeleteDoc(DocModel doc)
        {
            DocService.DeleteDoc(doc);
            docs.Remove(doc);
            StateHasChanged();
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

        public void GetAllSections()
        {
            sections = ProductionService.GetAllSections().ToList();
        }

        public void GetAllRowsBySection(string section)
        {
            rows = ProductionService.GetRowsBySection(section).ToList();
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

        public async void changeDocs(Radzen.UploadChangeEventArgs e)
        {
            foreach (var file in e.Files)
            {
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = @"E:\Zini Tecnologies Projects\MecuryProduct\wwwroot\uploads\" + $"stk-{car.Id}-doc-{datetime}-{file.Name}";
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

        private sealed class Instruction
        {
            public string label { get; set; } = string.Empty;
            public bool value { get; set; }
        }
    }
}
