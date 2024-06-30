using System.ComponentModel.DataAnnotations;

namespace MecuryProduct.Data
{
    public class InvoiceModel
    {
        public int Id { get; set; }

        public int quantity { get; set; }

        public int price { get; set; }

        public int? customer_id { get; set; }

        public int? company_id { get; set; }

        public string? created_by_id { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        public CustomerModel? customer {  get; set; }

        public CompanyModel? company { get; set; }

        public ApplicationUser? created_by { get; set; }

        public List<ProductInvoice>? productInvoice {  get; set; }
    }
}
