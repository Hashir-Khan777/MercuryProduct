using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;
using MecuryProduct.Modals;
using Radzen;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Radzen.Blazor.Rendering;

namespace MecuryProduct.Components.Employee.Pages
{
    public partial class Payments
    {
        List<PaymentModel> payments = new List<PaymentModel>();
        public string current_user = string.Empty;
        public int company_id;
        public Popup popup;
        public ElementReference button;

        [Inject]
        private PaymentService PaymentService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            GetPayments();

            var company = await SessionService.Get<int>("company");
            company_id = company;
            payments = PaymentService.GetPaymnetsByCompanyId(company).ToList();
        }

        public async void GetPayments()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    current_user = userId;
                }
            }
        }

        public void SearchPayments(dynamic args)
        {
            payments = PaymentService.SearchPaymentByCompanyId(args, company_id);
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
