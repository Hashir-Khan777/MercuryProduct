using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Inventory
    {
        private List<CarModel> cars = new List<CarModel>();
        private List<CarModel> selected_cars = new List<CarModel>();
        private List<StateFormModel> stateForms = new List<StateFormModel>();
        private bool IsShiftKey = false;

        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private StateFormService StateFormService { get; set; }

        protected override void OnInitialized()
        {
            GetCars();
            GeStateForms();
        }

        public void GetCars()
        {
            cars = CarService.GetCars().ToList().FindAll(c => c.status.ToLower() == "bought");
        }

        public void GeStateForms()
        {
            stateForms = StateFormService.Get().ToList();
        }

        public async void DeleteVehicle(CarModel car)
        {
            bool? deleteVehicle = await DialogService.Confirm("Are you sure?", "Do you want to delete vehicle?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteVehicle != null && deleteVehicle == true)
            {
                CarService.DeleteCar(car);
            }
            GetCars();
            StateHasChanged();
        }

        public async void DeleteStateForm(StateFormModel state_form)
        {
            bool? deleteStateForm = await DialogService.Confirm("Are you sure?", "Do you want to delete state form?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteStateForm != null && deleteStateForm == true)
            {
                StateFormService.Delete(state_form);
            }
            GeStateForms();
            StateHasChanged();
        }

        public async void OpenUpdateStateFormModal(int id)
        {
            await DialogService.OpenAsync<UpdateStateFormModal>("Update State Form",
                new Dictionary<string, object>() { { "SfId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async Task OpenBulkEditModal()
        {
            await DialogService.OpenAsync<BulkEditModal>("Bulk Update",
                new Dictionary<string, object>() { { "Cars", selected_cars } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void OpenImageModal(List<DocModel>? paths, bool slider)
        {
            await DialogService.OpenAsync<ImageModal>("",
                new Dictionary<string, object>() { { "Paths", paths ?? [] }, { "Slider", slider } },
                new DialogOptions() { Width = "90%", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        public async void OpenUpdateVehicleModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id }, { "Inventory", true } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            GetCars();
            StateHasChanged();
        }

        public async void OpenUpdateVehicleProductionModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id }, { "Production", true } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async Task OpenVehicleCommentModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Notes for m-veh-{VehId}",
                new Dictionary<string, object>() { { "VehId", VehId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        public async Task OpenDocsModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Docs for m-stk-{VehId}",
                new Dictionary<string, object>() { { "VehId", VehId }, { "Docs", true } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        public async Task OpenStateFormCommentModal(int EnvId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Notes for m-env-{EnvId}",
                new Dictionary<string, object>() { { "SfId", EnvId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        public bool GetValue(CarModel car)
        {
            int index = selected_cars.IndexOf(car);
            if (index != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MultiSelect(bool isChecked, CarModel car)
        {
            if (IsShiftKey && selected_cars.Count() > 0)
            {
                int start = cars.IndexOf(selected_cars[0]);
                int end = cars.IndexOf(car);

                if (start > end)
                {
                    var temp = start;
                    start = end;
                    end = temp;
                }

                for (var i = start; i <= end; i++)
                {
                    if (isChecked)
                    {
                        selected_cars.Add(cars[i]);
                    }
                    else
                    {
                        selected_cars.Remove(cars[i]);
                    }
                }
            }
            else
            {
                if (isChecked)
                {
                    selected_cars.Add(car);
                }
                else
                {
                    selected_cars.Remove(car);
                }
            }
            StateHasChanged();
        }

        public void KeyPress(KeyboardEventArgs e)
        {
            IsShiftKey = e.ShiftKey;
        }
    }
}
