using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Modals
{
    public partial class ShowCustomerVehiclesModal
    {
        [Parameter] public Object Id { get; set; }
        [Parameter] public string Role { get; set; }
        [Parameter] public DateTime? start_date { get; set; } = null;
        [Parameter] public DateTime? end_date { get; set; } = null;

        public CustomerModel customer = new CustomerModel();
        public ApplicationUser driver = new ApplicationUser();

        /// <summary>
        /// Gets or sets the CustomerService for handling customer-related operations.
        /// </summary>
        [Inject]
        public CustomerService CustomerService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private UserService DriverService { get; set; }

        /// <summary>
        /// This method is called when the element is initialized.
        /// It retrieves a customer by their ID.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCustomerById();
        }

        /// <summary>Retrieves customer or driver information by ID based on the role.</summary>
        /// <remarks>
        /// If the role is "customer", it retrieves customer information by ID and filters the cars based on the specified start and end dates.
        /// If the role is not "customer", it retrieves driver information by ID.
        /// </remarks>
        public void GetCustomerById()
        {
            if (Role.ToLower() == "customer")
            {
                var result = CustomerService.GetCustomerById((int)Id);
                if (result != null)
                {
                    if (start_date != null && end_date != null)
                    {
                        customer = result;
                        customer.cars = result.cars?.FindAll(c => c.scheduled_date.Date >= start_date?.Date && c.scheduled_date.Date <= end_date?.Date);
                    }
                    else
                    {
                        customer = result;
                    }
                }
            }
            else
            {
                var result = DriverService.GetUserById((string)Id);
                if (result != null)
                {
                    driver = result;
                }
            }
        }

        /// <summary>
        /// Opens a modal dialog to update customer information.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateCustomerModal(int id)
        {
            await DialogService.OpenAsync<UpdateCustomerModal>("Update Customer",
                new Dictionary<string, object>() { { "CusId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to update a driver with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the driver to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateDriverModal(string id)
        {
            await DialogService.OpenAsync<UpdateDriverModal>("Update Driver",
                new Dictionary<string, object>() { { "DriverId", id } },
                new DialogOptions() { Width = "600px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
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
                StateHasChanged();
            }
        }

        /// <summary>
        /// Opens a modal dialog to update a vehicle with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the vehicle to update.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async void OpenUpdateVehicleModal(int id)
        {
            await DialogService.OpenAsync<UpdateVehicleModal>("Update Vehicle",
                new Dictionary<string, object>() { { "VehId", id } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// <summary>
        /// Opens a modal dialog to display and edit comments for a specific vehicle.
        /// </summary>
        /// <param name="VehId">The ID of the vehicle for which comments are being displayed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OpenVehicleCommentModal(int VehId)
        {
            await DialogService.OpenAsync<VehicleCommentModal>($"Notes for m-veh-{VehId}",
                new Dictionary<string, object>() { { "VehId", VehId } },
                new DialogOptions() { Width = "700px", Height = "60%", Resizable = true, Draggable = true }
            );
        }
    }
}
