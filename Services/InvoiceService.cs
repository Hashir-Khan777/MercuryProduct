using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class InvoiceService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public InvoiceService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public void CreateInvoice(InvoiceModel invoice)
        {
            try
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public List<InvoiceModel>? GetInvoices()
        {
            try
            {
                return db.Invoices.Include(c => c.created_by).Include(c => c.company).Include(x => x.productInvoice).ThenInclude(x => x.product).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void AddProductInvoice(ProductInvoice ProductInvoice)
        {
            try
            {
                var alreadyExists = db.ProductInvoices.Any(x => x.invoice_id == ProductInvoice.invoice_id && x.product_id == ProductInvoice.product_id);
                if (!alreadyExists)
                {
                    db.ProductInvoices.Add(ProductInvoice);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void DeleteProductInvoice(ProductInvoice ProductInvoice)
        {
            try
            {
                var alreadyExists = db.ProductInvoices.Where(x => x.product_id == ProductInvoice.product_id && x.invoice_id == ProductInvoice.invoice_id);
                if (alreadyExists is not null)
                {
                    db.ProductInvoices.Remove(ProductInvoice);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }
    }
}
