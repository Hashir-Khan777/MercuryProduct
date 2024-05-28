using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class BulkEditModal
    {
        [Parameter] public List<CarModel> Cars { get; set; }

        private List<string> prod_status = new List<string>()
        {
            "Hold",
            "Set",
            "Pulled",
        };
        private List<string> sections = new List<string>();
        private List<string> rows = new List<string>();
        private BulkEditCarModel EditCarModel = new BulkEditCarModel();

        [Inject]
        private ProductionService ProductionService { get; set; }
        [Inject]
        private CarService CarService { get; set; }

        protected override void OnInitialized()
        {
            GetAllSections();
        }

        public void UpdateCars()
        {
            foreach (var car in Cars)
            {
                car.prod_status = EditCarModel.selected_status;
                if (EditCarModel.selected_section != null)
                {
                    car.section = EditCarModel.selected_section;
                }
                if (EditCarModel.selected_row != null)
                {
                    car.row = EditCarModel.selected_row;
                }
                if (EditCarModel.selected_status == "Set")
                {
                    car.set_date = DateTime.UtcNow;
                }
                if (EditCarModel.selected_status == "Pulled")
                {
                    car.pulled_date = DateTime.UtcNow;
                }
                car.updated_at = DateTime.UtcNow;
                CarService.UpdateCar(car);
            }
            dialogService.Close();
        }

        public void GetAllSections()
        {
            sections = ProductionService.GetAllSections().ToList();
        }

        public void GetAllRowsBySection(string section)
        {
            rows = ProductionService.GetRowsBySection(section).ToList();
        }

        private sealed class BulkEditCarModel
        {
            public string selected_status { get; set; } = string.Empty;
            public string? selected_section { get; set; } = null;
            public string? selected_row { get; set; } = null;
        }
    }
}
