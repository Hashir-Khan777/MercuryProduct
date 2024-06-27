using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /// Adds constraints to the model's constraints. This is used to determine which fields are allowed
    public class CompanyModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<CompanyManager>? CompanyManagers { get; set; }

        public List<CompanyEmployees>? CompanyEmployees { get; set; }

        public List<CompanyDrivers>? CompanyDrivers { get; set; }

        public List<CarModel>? Cars { get; set; }

        public List<CustomerModel>? Customers { get; set; }

        public List<StateFormModel>? StateForms { get; set; }

        public List<ProductModel>? Products { get; set; }
    }
}
