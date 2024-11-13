using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /// Adds constraints to the model's constraints. This is used to determine which fields are allowed
    public class CompanyModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string caddress { get; set; } = string.Empty;

        [Required]
        public double clat { get; set; }

        [Required]
        public double clon { get; set; }

        [Required]
        public string czip_code { get; set; } = string.Empty;

        [Required]
        public string ccity { get; set; } = string.Empty;

        [Required]
        public string ccountry { get; set; } = string.Empty;

        [Required]
        public string cstate { get; set; } = string.Empty;

        public List<CompanyManager>? CompanyManagers { get; set; }

        public List<CompanyEmployees>? CompanyEmployees { get; set; }

        public List<CompanyDrivers>? CompanyDrivers { get; set; }

        public List<CarModel>? Cars { get; set; }

        public List<CustomerModel>? Customers { get; set; }

        public List<StateFormModel>? StateForms { get; set; }

        public List<ProductModel>? Products { get; set; }

        public List<InvoiceModel>? Invoices { get; set; }

        public List<PaymentModel>? Payments { get; set; }

        public LocalizationModel? Localization { get; set; }

        public List<ExpenseModel>? expenses { get; set; }

        public List<PosCustomerModel>? pos_customers { get; set; }

        public string managers
        {
            get
            {
                var managerNames = new List<string>();

                managerNames.AddRange(CompanyManagers.Select(cm => cm.manager.UserName));

                return string.Join(", ", managerNames);
            }
        }
    }
}
