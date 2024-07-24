using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Payments
    {
        List<PaymentModel> payments = new List<PaymentModel>();

        [Inject]
        private PaymentService PaymentService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            payments = PaymentService.GetPayments();
        }

        public List<ProductModel> GetProucts(string products_json)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return JsonSerializer.Deserialize<List<ProductModel>>(products_json, options);
        }
    }
}
