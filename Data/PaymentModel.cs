namespace MecuryProduct.Data
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public int taxAmount { get; set; } = 0;

        public int discount { get; set; } = 0;

        public double itemsAmount { get; set; } = 0;

        public double totalAmount { get; set; } = 0;

        public string paymentType { get; set; } = string.Empty;

        public int paidAmount { get; set; } = 0;

        public int changeAmount { get; set; } = 0;

        public int? customerId {  get; set; }

        public CustomerModel? customer { get; set; }

        public string products { get; set; } = string.Empty;
    }
}
