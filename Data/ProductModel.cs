using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    /// Returns a copy of the model with all fields populated. Does not copy the data
    public class ProductModel
    {
        public int Id { get; set; }

        public string department_name { get; set; } = string.Empty;

        public string? created_by_id { get; set; } = string.Empty;

        public int? company_id { get; set; }

        public int quantity { get; set; }

        public string product_name { get; set; } = string.Empty;

        public string product_description { get; set; } = string.Empty;

        public int regular_price { get; set; }

        public int custom_price_1 { get; set; }

        public int custom_price_2 { get; set; }

        public int custom_price_3 { get; set; }

        public int custom_price_4 { get; set; }

        public string product_grade { get; set; } = string.Empty;

        public List<DocModel>? images { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public ApplicationUser? created_by { get; set; }

        public CompanyModel? company { get; set; }
    }
}
