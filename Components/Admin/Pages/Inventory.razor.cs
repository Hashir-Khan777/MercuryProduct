using MecuryProduct.Data;
using MecuryProduct.Modals;
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

        /// <summary>Injects dependencies for the current class.</summary>
        /// <remarks>
        /// Injects the <see cref="CarService"/>, <see cref="DialogService"/>, and <see cref="StateFormService"/> 
        /// dependencies into the current class.
        /// </remarks>
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private StateFormService StateFormService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// This method is called when the object is initialized.
        /// It retrieves the list of cars and state forms.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCars();
            GeStateForms();
        }

        /// <summary>
        /// Retrieves a list of cars that have been bought.
        /// </summary>
        /// <remarks>
        /// This method fetches the list of cars from the CarService, filters the list to include only cars with the status "bought", 
        /// and assigns the filtered list to the 'cars' field.
        /// </remarks>
        public async void GetCars()
        {
            var company = await SessionService.Get<int>("company");
            cars = CarService.GetCarsByCompanyId(company).ToList().FindAll(c => c.status.ToLower() == "bought");
        }

        /// <summary>
        /// Retrieves a list of state forms.
        /// </summary>
        /// <remarks>
        /// This method fetches the state forms from the StateFormService and stores them in the stateForms variable.
        /// </remarks>
        public async void GeStateForms()
        {
            var company = await SessionService.Get<int>("company");
            stateForms = StateFormService.GetByCompanyId(company).ToList();
        }

        /// <summary>
        /// Deletes a vehicle from the system after confirming with the user.
        /// </summary>
        /// <param name="car">The car model to be deleted.</param>
        /// <returns>Void</returns>
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

        /// <summary>
        /// Deletes a state form after confirming with the user.
        /// </summary>
        /// <param name="state_form">The state form model to be deleted.</param>
        /// <returns>Void</returns>
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

        /// <summary>
        /// Opens a modal dialog to update the state form with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the state form to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateStateFormModal(int id)
        {
            await DialogService.OpenAsync<UpdateStateFormModal>("Update State Form",
                new Dictionary<string, object>() { { "SfId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal for bulk editing of selected cars.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenBulkEditModal()
        {
            await DialogService.OpenAsync<BulkEditModal>("Bulk Update",
                new Dictionary<string, object>() { { "Cars", selected_cars } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens an image modal dialog with the specified paths and slider option.
        /// </summary>
        /// <param name="paths">A list of paths to the images to display.</param>
        /// <param name="slider">A boolean indicating whether to enable a slider in the modal.</param>
        /// <remarks>
        /// The method asynchronously opens an image modal dialog using the DialogService.
        /// The dialog displays images based on the provided paths and includes a slider if specified.
        /// </remarks>
        public async void OpenImageModal(List<DocModel>? paths, bool slider)
        {
            await DialogService.OpenAsync<ImageModal>("",
                new Dictionary<string, object>() { { "Paths", paths ?? [] }, { "Slider", slider } },
                new DialogOptions() { Width = "90%", Height = "90%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>
        /// Opens a modal dialog to update a vehicle with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle to update.</param>
        /// <remarks>
        /// This method opens a modal dialog for updating a vehicle with the given ID. 
        /// It uses the DialogService to display the UpdateVehicleModal component with the provided parameters.
        /// After opening the modal, it calls the GetCars method to refresh the list of cars and then triggers a UI update.
        /// </remarks>
        public async void OpenUpdateVehicleModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id }, { "Inventory", true } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            // PP-100: real time update in inventory
            // Bug: inventory data is not updating on real time
            // Fix: Add GetCars function in update function
            GetCars();
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to update the production status of a vehicle.
        /// </summary>
        /// <param name="id">The ID of the vehicle to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateVehicleProductionModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id }, { "Production", true } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to display comments for a specific vehicle.
        /// </summary>
        /// <param name="VehId">The ID of the vehicle for which comments are to be displayed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenVehicleCommentModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Notes for m-veh-{VehId}",
                new Dictionary<string, object>() { { "VehId", VehId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>Opens a modal dialog to display documents related to a specific vehicle.</summary>
        /// <param name="VehId">The ID of the vehicle for which documents are to be displayed.</param>
        /// <returns>An asynchronous task representing the operation.</returns>
        public async Task OpenDocsModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Docs for m-stk-{VehId}",
                new Dictionary<string, object>() { { "VehId", VehId }, { "Docs", true } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>Opens a modal dialog to add or view comments for a specific environment.</summary>
        /// <param name="EnvId">The ID of the environment.</param>
        /// <returns>An asynchronous task representing the operation.</returns>
        public async Task OpenStateFormCommentModal(int EnvId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Notes for m-env-{EnvId}",
                new Dictionary<string, object>() { { "SfId", EnvId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }

        /// <summary>Determines if a CarModel object is present in the list of selected cars.</summary>
        /// <param name="car">The CarModel object to check for.</param>
        /// <returns>True if the CarModel object is found in the list of selected cars, false otherwise.</returns>
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

        /// <summary>
        /// Allows for multi-select functionality based on the shift key and a given CarModel.
        /// </summary>
        /// <param name="isChecked">A boolean indicating whether the item is checked or not.</param>
        /// <param name="car">The CarModel to be selected or deselected.</param>
        /// <remarks>
        /// If the shift key is pressed and there are previously selected cars, the method selects a range of cars
        /// from the first selected car to the current car. Otherwise, it simply selects or deselects the given car.
        /// </remarks>
        // PP-76: Bulk edit feature with checkbox selection + shift select functionality like Jira in inventory module
        // Feature: Add bulk edit functionality like jira
        // Fix: When checkbox value changes, I am pushing the car on selected_cars list and then opens the bulk edit modal and loop throught the selected_cars to edit multiple cars at a time
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

        /// <summary>
        /// Sets the state of the Shift key based on the provided KeyboardEventArgs.
        /// </summary>
        /// <param name="e">The KeyboardEventArgs containing information about the key press event.</param>
        public void KeyPress(KeyboardEventArgs e)
        {
            IsShiftKey = e.ShiftKey;
        }
    }
}
