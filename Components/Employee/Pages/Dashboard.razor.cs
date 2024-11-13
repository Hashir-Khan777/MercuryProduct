using MecuryProduct.Components.Admin.Pages;
using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Employee.Pages
{
    public class AvgCarOfferByCatModel
    {
        public string title { get; set; } = string.Empty;
        public int totalCars { get; set; }
        public int avgAmount { get; set; }
    }

    public partial class Dashboard
    {
        private List<AvgCarOfferByCatModel> data = new List<AvgCarOfferByCatModel>();
        private List<CarModel> cars = new List<CarModel>();
        private List<string> vehicle_type = new List<string>()
        {
            "Builder",
            "SP",
            "Yard",
            "Full Service",
        };

        /// <summary>Gets or sets the CarService dependency for the current class.</summary>
        /// <remarks>This property is annotated with the [Inject] attribute to indicate dependency injection.</remarks>
        [Inject]
        private CarService CarService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// <summary>
        /// Initializes the object by retrieving car data, calculating average values for each vehicle type, and calculating the total average.
        /// </summary>
        protected override void OnInitialized()
        {
            GetCars();
            foreach (var cat in vehicle_type)
            {
                GetAverage(cat);
            }
            GetTotalAvg();
        }

        /// <summary>
        /// Retrieves a list of cars from the CarService and stores them in the 'cars' field.
        /// </summary>
        public async void GetCars()
        {
            var company = await SessionService.Get<int>("company");
            cars = CarService.GetCarsByCompanyId(company).ToList();
        }

        /// <summary>Calculates the average offered amount for cars of a specific category.</summary>
        /// <param name="cat">The category of cars to calculate the average for.</param>
        /// <remarks>This method filters cars by the specified category, calculates the average offered amount,
        /// and adds the result to the data collection.</remarks>
        public void GetAverage(string cat)
        {
            var filterdByCategory = cars.FindAll(c => c.vehicle_type.ToLower() == cat.ToLower());
            int avgAmount = 0;
            foreach (var car in cars)
            {
                if (car.vehicle_type.ToLower() == cat.ToLower())
                {
                    if (car.offered_ammount is not null)
                    {
                        avgAmount += (int)car.offered_ammount;
                    }
                }
            }

            if (filterdByCategory.Count() > 0 && avgAmount > 0)
            {
                string title = cat;
                var avg = new AvgCarOfferByCatModel() { totalCars = filterdByCategory.Count(), avgAmount = (avgAmount / filterdByCategory.Count()), title = title };
                data.Add(avg);
            }
        }

        /// <summary>
        /// Calculates the total average car offer amount by category.
        /// </summary>
        /// <returns>
        /// An instance of AvgCarOfferByCatModel containing the total number of cars, the average offer amount,
        /// and a title indicating the total average, or null if there are no cars or the average amount is zero.
        /// </returns>
        public AvgCarOfferByCatModel? GetTotalAvg()
        {
            int avgAmount = 0;
            foreach (var car in cars)
            {
                if (car.offered_ammount is not null)
                {
                    avgAmount += (int)car.offered_ammount;
                }
            }

            if (cars.Count() > 0 && avgAmount > 0)
            {
                string title = "Total";
                return new AvgCarOfferByCatModel() { totalCars = cars.Count(), avgAmount = (avgAmount / cars.Count()), title = title };
            }
            else
            {
                return null;
            }
        }
    }
}
