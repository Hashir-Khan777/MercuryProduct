using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MecuryProduct.Modals
{
    public partial class ViewSalesHistory
    {
        [Parameter] public int CusId {  get; set; }

        public List<PaymentModel> payments = new List<PaymentModel>();
        public LocalizationModel localization = new LocalizationModel();

        [Inject]
        public PaymentService PaymentService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; } 

        protected override async void OnInitialized()
        {
            payments = PaymentService.GetPaymnetsByCustomerId(CusId);

            base.OnInitialized();
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
