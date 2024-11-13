using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Invoices
    {
        public List<InvoiceModel> invoices = new List<InvoiceModel>();

        [Inject]
        public InvoiceService InvoiceService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            invoices = InvoiceService.GetInvoices();
        }
    }
}
