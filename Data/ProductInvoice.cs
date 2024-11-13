namespace MecuryProduct.Data
{
    public class ProductInvoice
    {
        public int Id { get; set; }

        public int? product_id { get; set; }

        public ProductModel? product { get; set; }

        public int? invoice_id { get; set; }

        public InvoiceModel? invoice { get; set; }
    }
}
