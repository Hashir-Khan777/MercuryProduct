using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class CompanyModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string? ManagerId { get; set; }

        public ApplicationUser? Manager { get; set; }

        public List<ApplicationUser>? Employees { get; set; }

        public List<CarModel>? Cars { get; set; }

        public List<CustomerModel>? Customers { get; set; }

        public List<StateFormModel>? StateForms { get; set; }
    }
}
