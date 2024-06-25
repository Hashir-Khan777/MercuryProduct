using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        public ProductService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        public List<ProductModel>? GetProducts()
        {
            try
            {
                return db.Products.Include("created_by").Include("images").ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByManagerId(string UserId)
        {
            try
            {
                return db.Products.Where(p => p.company.ManagerId == UserId).Include("created_by").Include("images").ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByCompanyId(int? CompId)
        {
            try
            {
                return db.Products.Where(p => p.company_id == CompId).Include("created_by").Include("images").ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public ProductModel? GetProductById(int ProdId)
        {
            try
            {
                return db.Products.Include("created_by").Include("images").FirstOrDefault(x => x.Id == ProdId);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void AddProduct(ProductModel Product)
        {
            try
            {
                db.Products.Add(Product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void DeleteProduct(ProductModel Product)
        {
            try
            {
                db.Products.Remove(Product);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void UpdateProduct(ProductModel Product)
        {
            try
            {
                db.Products.Update(Product);
                db.SaveChanges();
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
