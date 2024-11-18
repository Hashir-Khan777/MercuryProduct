using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /* The CustomerModel class represents a customer entity with various properties including personal
    information, contact details, and related entities like cars and notes. */
    public class CustomerModel
    {
        public int Id { get; set; }

        public bool deleted { get; set; } = false;

        [Required]
        public string cfname { get; set; } = string.Empty;

        [Required]
        public string clname { get; set; } = string.Empty;

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

        [Required]
        public string cphone_number { get; set; } = string.Empty;

        public string number_type { get; set; } = string.Empty;

        public string? created_by_id { get; set; } = string.Empty;

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public List<string> contact_prefrence { get; set; } = new List<string>();

        [Required]
        public string email { get; set; } = string.Empty;

        public string customer_type { get; set; } = string.Empty;

        [Required]
        public string company_name { get; set; } = string.Empty;

        public string customer_notes { get; set; } = string.Empty;

        public string search { get; set; } = string.Empty;

        public int? CompanyId { get; set; }

        public CompanyModel? Company { get; set; }

        public ApplicationUser? created_by { get; set; }

        public List<CarModel>? cars { get; set; }

        public List<NoteModel>? notes { get; set; }

        public List<InvoiceModel>? invoices { get; set; }

        public List<PaymentModel>? payments { get; set; }

        public List<ExpenseModel>? expenses { get; set; }

        public int Car_Count
        {
            get
            {
                if (cars?.Count() > 0)
                {
                    return cars.Count();
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
