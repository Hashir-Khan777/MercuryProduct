namespace MecuryProduct.Data
{
    public class LocalizationModel
    {
        public int Id { get; set; }

        public double salesTax { get; set; } = 0;

        public double bentonCountyTax { get; set; } = 0;

        public double cityOfRogersTax { get; set; } = 0;
         
        public double epaFeesTax { get; set; } = 0;

        public double stateOfArkansaTax { get; set; } = 0;

        public string tax_1_label { get; set; } = string.Empty;

        public double tax_1_value { get; set; } = 0;

        public string tax_2_label { get; set; } = string.Empty;

        public double tax_2_value { get; set; } = 0;

        public string tax_3_label { get; set; } = string.Empty;

        public double tax_3_value { get; set; } = 0;

        public string tax_4_label { get; set; } = string.Empty;

        public double tax_4_value { get; set; } = 0;

        public string defaultProductImage { get; set; } = string.Empty;

        public string companyLogo { get; set; } = string.Empty;

        public string showPrice { get; set; } = string.Empty;

        public int? company_id { get; set; }

        public CompanyModel? company { get; set; }
    }
}
