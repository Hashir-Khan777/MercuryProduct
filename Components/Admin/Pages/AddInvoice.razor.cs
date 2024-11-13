using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddInvoice
    {
        public InvoiceModel invoice = new InvoiceModel();
        public List<ProductModel> products = new List<ProductModel>();
        public List<CustomerModel> customers = new List<CustomerModel>();
        public List<CompanyModel> companies = new List<CompanyModel>();
        public List<int> selected_products = new List<int>();

        [Inject]
        public CompanyService CompanyService { get; set; }
        [Inject]
        public CustomerService CustomerService { get; set; }
        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public InvoiceService InvoiceService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            SetUserId();

            var result = await SessionService.Get<InvoiceModel>("invoice_form");

            companies = CompanyService.GetCompanies();
            var company = await SessionService.Get<int>("company");
            customers = CustomerService.GetCustomersByCompanyId(company);
            products = ProductService.GetProductsByCompanyId(company);

            if (result != null)
            {
                invoice = result;
            }
        }

        public void CreateInvoice() {
            InvoiceService.CreateInvoice(invoice);
            foreach (var product in selected_products)
            {
                InvoiceService.AddProductInvoice(new ProductInvoice { invoice_id = invoice.Id, product_id = product });
            }
            NavigationManager.NavigateTo("/admin/invoices");
        }

        public async void SetInSession()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            await SessionService.Set("invoice_form", JsonSerializer.Serialize(invoice, options));
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    invoice.created_by_id = userId;
                }
            }
        }
    }
}
