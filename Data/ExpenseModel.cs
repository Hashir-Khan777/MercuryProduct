namespace MecuryProduct.Data
{
    public class ExpenseModel
    {
        public int Id { get; set; }

        public string type { get; set; } = string.Empty;

        public int cus_id { get; set; }

        public int company_id { get; set; }

        public string created_by_id { get; set; } = string.Empty;

        public string? reciept_image { get; set; } = string.Empty;

        public string? notes { get; set; } = string.Empty;

        public string payment_type { get; set; } = string.Empty;

        public double amount { get; set; }

        public string? check_no { get; set; } = string.Empty;

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public ApplicationUser? created_by { get; set; }

        public CustomerModel? customer { get; set; }

        public CompanyModel? company { get; set; }
    }
}
