using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class POS
    {
        public List<ProductModel> products = new List<ProductModel>();
        public List<CategoryModel> categories = new List<CategoryModel>();
        int page = 1;
        int company_id;
        string search = "";

        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        public CategoryService CategoryService { get; set; }
        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public LocalizationService LocalizationService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            var company = await SessionService.Get<int>("company");
            company_id = company;
            products = ProductService.GetProductsByCompanyId(company);
            categories = CategoryService.GetCategories();
        }

        public LocalizationModel GetLocalization(int? companyId)
        {
            return LocalizationService.GetLocalizationByCompanyId(companyId);
        }

        public double GetPrice(ProductModel product)
        {
            return (double)product.GetType().GetProperty(GetLocalization(product.company_id).showPrice)?.GetValue(product, null);
        }

        public void FilterProducts()
        {
            products = ProductService.FilterProducts(search.ToLower());
            StateHasChanged();
        }

        public List<ProductModel> GetProductsByCategory(int? id)
        {
           return ProductService.GetProductsByCategoryIdAndCompanyId(id, company_id).ToList();
        }

        public void LoadMore()
        {
            ++page;
            products = ProductService.GetProductsPagination(page);
            StateHasChanged();
        }

        public async void ChangeSearch(dynamic product)
        {
            var existing_products = await SessionService.Get<List<ProductModel>>("cart");
            bool alreadyInCart = false;
            List<ProductModel> products = new List<ProductModel>();

            if (existing_products is not null && existing_products.Count() > 0)
            {
                alreadyInCart = existing_products.Any(x => x.Id == product.Id);
                products.AddRange(existing_products);
                products.Add(product);
            }
            else
            {
                products.Add(product);
            }

            if (!alreadyInCart)
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                await SessionService.Set("cart", JsonSerializer.Serialize(products, options));
            }
            StateHasChanged();
            NavigationManager.Refresh(true);
        }

        public async void AddToCart(ProductModel product)
        {
            var existing_products = await SessionService.Get<List<ProductModel>>("cart");
            bool alreadyInCart = false;
            List<ProductModel> products = new List<ProductModel>();

            if (existing_products is not null && existing_products.Count() > 0)
            {
                alreadyInCart = existing_products.Any(x => x.Id == product.Id);
                products.AddRange(existing_products);
                products.Add(product);
            }
            else
            {
                products.Add(product);
            }

            if (!alreadyInCart)
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                await SessionService.Set("cart", JsonSerializer.Serialize(products, options));
            }
            StateHasChanged();
            NavigationManager.Refresh(true);
        }
    }
}
