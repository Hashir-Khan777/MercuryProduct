using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MecuryProduct.Modals
{
    public partial class PDFModal
    {
        [Inject]
        private IJSRuntime JS { get; set; }
        [Inject]
        private PaymentService PaymentService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        public LocalizationService LocalizationService { get; set; }

        [Parameter] public int PaymentId { get; set; }

        public List<ProductModel> products = new List<ProductModel>();
        public PaymentModel payment = new PaymentModel();
        public ElementReference ItemToExport;
        public LocalizationModel localization = new LocalizationModel();

        protected override async void OnInitialized()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            payment = PaymentService.GetPaymentById(PaymentId);
            products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
            if (products is not null && products.Count() > 0)
            {
                localization = GetLocalization(products[0].company_id);
            }

            base.OnInitialized();
        }

        public LocalizationModel GetLocalization(int? companyId)
        {
            return LocalizationService.GetLocalizationByCompanyId(companyId);
        }

        public double GetPrice(ProductModel product)
        {
            return (double)product.GetType().GetProperty(GetLocalization(product.company_id).showPrice)?.GetValue(product, null);
        }

        public double GetAmount(ProductModel item)
        {
            var key = item.GetType().GetProperty(item.show_price);
            if (key is not null)
            {
                var showPrice = key.GetValue(item, null);
                double price = item.special ? (double)item.special_price : (double)showPrice;
                double tax = (double)item.tax_1_value + (double)item.tax_2_value + (double)item.tax_3_value + (double)item.tax_4_value;
                var totalPrice = (price + ((price * tax) / 100)) * item.incartquantity;
                var discounted_amount = (totalPrice * item.discount ?? 0) / 100;
                return totalPrice - discounted_amount;
            }
            return 0;
        }

        public async void save()
        {
            await JS.InvokeVoidAsync("generatePDF", ItemToExport, payment.Id);
            dialogService.Close();
        }
    }
}
