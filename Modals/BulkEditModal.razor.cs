using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Modals
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

        /// <summary>Injects the ProductionService and CarService dependencies.</summary>
        [Inject]
        private ProductionService ProductionService { get; set; }
        [Inject]
        private CarService CarService { get; set; }

        /// <summary>
        /// This method is called when the element is initialized.
        /// It triggers the retrieval of all sections.
        /// </summary>
        protected override void OnInitialized()
        {
            GetAllSections();
        }

        /// <summary>Updates the properties of cars based on the selected values in the EditCarModel.</summary>
        /// <remarks>
        /// This method iterates through each car in the Cars collection and updates its production status,
        /// section, row, set date, pulled date, and last updated timestamp based on the selected values in the EditCarModel.
        /// </remarks>
        /// <seealso cref="CarService.UpdateCar(Car)"/>
        /// <seealso cref="dialogService.Close()"/>
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
        /// Retrieves all rows by a specified section and assigns them to the 'rows' property.
        /// </summary>
        /// <param name="section">The section to retrieve rows from.</param>
        public void GetAllRowsBySection(string section)
        {
            EditCarModel.selected_row = null;
            rows = ProductionService.GetRowsBySection(section).ToList();
        }

        /// <summary>
        /// Represents a model for bulk editing car data.
        /// </summary>
        private sealed class BulkEditCarModel
        {
            public string selected_status { get; set; } = string.Empty;
            public string? selected_section { get; set; } = null;
            public string? selected_row { get; set; } = null;
        }
    }
}
