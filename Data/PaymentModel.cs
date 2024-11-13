namespace MecuryProduct.Data
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public double taxAmount { get; set; } = 0;

        public double discount { get; set; } = 0;

        public int cartDiscount { get; set; } = 0;

        public double itemsAmount { get; set; } = 0;

        public double totalAmount { get; set; } = 0;

        public string paymentType { get; set; } = string.Empty;

        public int paidAmount { get; set; } = 0;

        public int changeAmount { get; set; } = 0;

        public int? customerId {  get; set; }

        public int? company_id { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public PosCustomerModel? customer { get; set; }

        public CompanyModel? company { get; set; }

        public string products { get; set; } = string.Empty;
    }
}
