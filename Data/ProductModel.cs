using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /// Returns a copy of the model with all fields populated. Does not copy the data
    public class ProductModel
    {
        public int Id { get; set; }

        public string? created_by_id { get; set; } = string.Empty;

        public int? company_id { get; set; }

        public int quantity { get; set; }

        public int incartquantity { get; set; } = 1;

        public int? discount { get; set; } = 0;

        public string product_name { get; set; } = string.Empty;

        public string industry_code { get; set; } = string.Empty;

        public string product_description { get; set; } = string.Empty;

        public bool special { get; set; } = false;

        public double special_price { get; set; }

        public double regular_price { get; set; }

        public double custom_price_1 { get; set; }

        public double custom_price_2 { get; set; }

        public double custom_price_3 { get; set; }

        public double custom_price_4 { get; set; }

        public double vat { get; set; }

        public string product_grade { get; set; } = string.Empty;

        public int? CategoryId { get; set; }

        public double? bentonCountyTax { get; set; } = 0;

        public double? cityOfRogersTax { get; set; } = 0;

        public double? epaFeesTax { get; set; } = 0;

        public double? stateOfArkansaTax { get; set; } = 0;

        public string tax_1_label { get; set; } = string.Empty;

        public double tax_1_value { get; set; } = 0;

        public string tax_2_label { get; set; } = string.Empty;

        public double tax_2_value { get; set; } = 0;

        public string tax_3_label { get; set; } = string.Empty;

        public double tax_3_value { get; set; } = 0;

        public string tax_4_label { get; set; } = string.Empty;

        public double tax_4_value { get; set; } = 0;

        public double? totalTax { get; set; } = 0;

        public string? show_price { get; set; }

        public double? taxAmount { get; set; }

        public bool? returned { get; set; } = false;

        public DateTime? returned_at { get; set; }

        public List<DocModel>? images { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public ApplicationUser? created_by { get; set; }

        public CategoryModel? category { get; set; }

        public CompanyModel? company { get; set; }

        public List<ProductInvoice>? productInvoice { get; set; }
    }
}
