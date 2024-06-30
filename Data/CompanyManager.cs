namespace MecuryProduct.Data
{
    public class CompanyManager
    {
        public int Id { get; set; }

        public string? manager_id { get; set; }

        public ApplicationUser? manager { get; set; }

        public int? company_id { get; set; }

        public CompanyModel? company { get; set; }
    }
}
