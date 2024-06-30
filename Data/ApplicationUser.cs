using Microsoft.AspNetCore.Identity;

namespace MecuryProduct.Data
{
    /* The `ApplicationUser` class in C# represents a user with properties such as driverId, password,
    oldThreePasswords, cars, driver_cars, notes, and customers. */
    public class ApplicationUser : IdentityUser
    {
        public int? driverId { get; set; }

        public string password { get; set; } = string.Empty;
        
        public List<string> oldThreePasswords { get; set; } = new List<string>();

        public List<string>? permissions { get; set; } = new List<string>();

        public List<CompanyEmployees>? CompanyEmployees { get; set; }

        public List<CompanyDrivers>? CompanyDrivers { get; set; }

        public List<CarModel>? cars { get; set; }
        
        public List<CarModel>? driver_cars { get; set; }
        
        public List<NoteModel>? notes { get; set; }
        
        public List<CustomerModel>? customers { get; set; }

        public List<CompanyManager>? CompanyManagers { get; set; }

        public List<AuditLogModel>? logs { get; set; }

        public List<ProductModel>? products { get; set; }

        public List<InvoiceModel>? invoices { get; set; }
    }
}
