namespace MecuryProduct.Data
{
    public class CompanyEmployees
    {
        public int Id { get; set; }

        public string? employee_id { get; set; }

        public ApplicationUser? employee { get; set; }

        public int? company_id { get; set; }

        public CompanyModel? company { get; set; }
    }
}
