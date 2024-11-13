using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class PosCustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string cfname { get; set; } = string.Empty;

        [Required]
        public string clname { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public bool loyalty_member { get; set; } = false;

        public string caddress { get; set; } = string.Empty;

        public double clat { get; set; }

        public double clon { get; set; }

        public string czip_code { get; set; } = string.Empty;

        public string ccity { get; set; } = string.Empty;

        public string ccountry { get; set; } = string.Empty;

        public string cstate { get; set; } = string.Empty;

        public string cphone_number { get; set; } = string.Empty;

        public string? created_by_id { get; set; } = string.Empty;

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public string search { get; set; } = string.Empty;

        public int? CompanyId { get; set; }

        public CompanyModel? Company { get; set; }

        public ApplicationUser? created_by { get; set; }

        public List<PaymentModel>? payments { get; set; }
    }
}
