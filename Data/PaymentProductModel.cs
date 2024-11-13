using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MecuryProduct.Data
{
    public class PaymentProductModel
    {
        public int Id { get; set; }

        public string? created_by_id { get; set; } = string.Empty;

        public int? company_id { get; set; }

        public int quantity { get; set; }

        public int incartquantity { get; set; } = 1;

        public int? discount { get; set; } = 0;

        public string product_name { get; set; } = string.Empty;

        public string product_description { get; set; } = string.Empty;

        public int regular_price { get; set; }

        public int custom_price_1 { get; set; }

        public int custom_price_2 { get; set; }

        public int custom_price_3 { get; set; }

        public int custom_price_4 { get; set; }

        public int taxAmount { get; set; }

        public int discountAmount { get; set; }

        public bool returned { get; set; } = false;

        public string product_grade { get; set; } = string.Empty;

        public int? CategoryId { get; set; }

        public DateTime? returned_at { get; set; }

        [NotMapped]
        public List<DocModel>? images { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        [NotMapped]
        public ApplicationUser? created_by { get; set; }

        [NotMapped]
        public CategoryModel? category { get; set; }

        [NotMapped]
        public CompanyModel? company { get; set; }

        [NotMapped]
        public List<ProductInvoice>? productInvoice { get; set; }
    }
}
