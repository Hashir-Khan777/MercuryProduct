using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public class AvgCarOfferByCatModel
    {
        public string title {  get; set; } = string.Empty;
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

        [Inject]
        private CarService CarService { get; set; }

        protected override void OnInitialized()
        {
            GetCars();
            foreach (var cat in vehicle_type)
            {
                GetAverage(cat);
            }
            GetTotalAvg();
        }

        public void GetCars()
        {
            cars = CarService.GetCars().ToList();
        }

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
