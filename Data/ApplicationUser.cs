using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MecuryProduct.Data
{
    /* The `ApplicationUser` class in C# represents a user with properties such as driverId, password,
    oldThreePasswords, cars, driver_cars, notes, and customers. */
    public class ApplicationUser : IdentityUser
    {
        public int? driverId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int user_id { get; set; }

        public string password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool deleted { get; set; } = false;

        public List<string> oldThreePasswords { get; set; } = new List<string>();

        public List<string>? permissions { get; set; } = new List<string>();

        public List<CompanyEmployees>? CompanyEmployees { get; set; }

        public List<CompanyDrivers>? CompanyDrivers { get; set; }

        public List<CarModel>? cars { get; set; }
        
        public List<CarModel>? driver_cars { get; set; }
        
        public List<NoteModel>? notes { get; set; }
        
        public List<CustomerModel>? customers { get; set; }

        public List<PosCustomerModel>? pos_customers { get; set; }

        public List<CompanyManager>? CompanyManagers { get; set; }

        public List<AuditLogModel>? logs { get; set; }

        public List<ProductModel>? products { get; set; }

        public List<InvoiceModel>? invoices { get; set; }

        public List<ExpenseModel>? expenses { get; set; }

        public int car_count
        {
            get
            {
                return cars?.Count() ?? 0;
            }
        }

        public string Lockout
        {
            get
            {
                return LockoutEnd == null ? "No" : "Yes";
            }
        }

        public string CompanyNames
        {
            get
            {
                var companyNames = new List<string>();

                if (CompanyManagers.Count > 0)
                    companyNames.AddRange(CompanyManagers.Select(cm => cm.company.Name));

                if (CompanyEmployees.Count > 0)
                    companyNames.AddRange(CompanyEmployees.Select(ce => ce.company.Name));

                if (CompanyDrivers.Count > 0)
                    companyNames.AddRange(CompanyDrivers.Select(cd => cd.company.Name));

                return string.Join(", ", companyNames);
            }
        }
    }
}
