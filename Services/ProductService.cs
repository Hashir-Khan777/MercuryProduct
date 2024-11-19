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
                return db.Products.Include(c => c.created_by).Include(c => c.category).Include(c => c.images).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetAllProductsByCompanyId(int company_id)
        {
            try
            {
                return db.Products.Include(c => c.created_by).Include(c => c.category).Include(c => c.images).Where(x => x.company_id == company_id).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByCompanyId(int company_id)
        {
            try
            {
                return db.Products.Include(c => c.created_by).Include(c => c.category).Include(c => c.images).Where(x => x.company_id == company_id && !x.deleted).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? SearchProducts(string query)
        {
            try
            {
                return db.Products.Include(c => c.created_by).Include(c => c.category).Include(c => c.images).Where(c => c.product_name.Contains(query) || c.category.Name.Contains(query)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? SearchProductsByCompanyId(string query, int company_id)
        {
            try
            {
                return db.Products.Where(x => x.company_id == company_id).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).Where(c => c.product_name.Contains(query) || c.category.Name.Contains(query)).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsPagination(int page)
        {
            try
            {
                return db.Products.Take(page * 30).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? FilterProducts(string search)
        {
            try
            {
                return db.Products.Where(x => x.product_name.ToLower().Contains(search) || x.category.Name.ToLower().Contains(search) || x.product_description.ToLower().Contains(search)).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByManagerId(string ManagerId)
        {
            try
            {
                return db.Products.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company.CompanyManagers.Any(x => x.manager_id == ManagerId)).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByEmployeeId(string EmployeeId)
        {
            try
            {
                return db.Products.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).ToList();
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

        public List<ProductModel>? GetProductsByCategoryId(int? CatId)
        {
            try
            {
                return db.Products.Where(p => p.CategoryId == CatId).Include("created_by").Include("images").ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByCategoryIdAndCompanyId(int? CatId, int company_id)
        {
            try
            {
                return db.Products.Where(p => p.CategoryId == CatId && p.company_id == company_id).Include("created_by").Include("images").ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByCategoryIdAndManagerId(int? CatId, string ManagerId)
        {
            try
            {
                return db.Products.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company.CompanyManagers.Any(x => x.manager_id == ManagerId)).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).Where(p => p.CategoryId == CatId).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: $"{ex}");
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ProductModel>? GetProductsByCategoryIdAndEmployeeId(int? CatId, string EmployeeId)
        {
            try
            {
                return db.Products.Include(c => c.company).ThenInclude(c => c.CompanyManagers).Where(p => p.company.CompanyEmployees.Any(x => x.employee_id == EmployeeId)).Include(c => c.created_by).Include(c => c.category).Include(c => c.images).Where(p => p.CategoryId == CatId).ToList();
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
                Product.deleted = true;
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
