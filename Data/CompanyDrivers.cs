namespace MecuryProduct.Data
{
    public class CompanyDrivers
    {
        public int Id { get; set; }

        public string? driver_id { get; set; }

        public ApplicationUser? driver { get; set; }

        public int? company_id { get; set; }

        public CompanyModel? company { get; set; }
    }
}
