using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;
using MecuryProduct.Modals;
using Radzen;
using Radzen.Blazor.Rendering;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Payments
    {
        List<PaymentModel> payments = new List<PaymentModel>();
        public Popup popup;
        public ElementReference button;

        [Inject]
        private PaymentService PaymentService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            var company = await SessionService.Get<int>("company");
            payments = PaymentService.GetPaymnetsByCompanyId(company);
        }

        public async void SearchPayments(dynamic args)
        {
            var company = await SessionService.Get<int>("company");
            payments = PaymentService.SearchPaymentByCompanyId(args, company);
        }

        public async void RePrintPDF(int Id)
        {
            await DialogService.OpenAsync<PDFModal>("Save PDF",
                new Dictionary<string, object>() { { "PaymentId", Id } },
                new DialogOptions() { Width = "70%", Height = "90%", Resizable = true, Draggable = true }
            );
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

        public async void ViewPayment(int id)
        {
            await DialogService.OpenAsync<ViewPaymentModal>("Sale " + id,
                new Dictionary<string, object>() { { "PaymentId", id } },
                new DialogOptions() { Width = "70%", Height = "90%", Resizable = true, Draggable = true }
            );
            var company = await SessionService.Get<int>("company");
            payments = PaymentService.GetPaymnetsByCompanyId(company);
            StateHasChanged();
        }
    }
}
