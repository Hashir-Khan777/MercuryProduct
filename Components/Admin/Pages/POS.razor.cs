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
        int page = 1;
        string search = "";

        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            products = ProductService.GetProductsPagination(page);
        }

        public void FilterProducts()
        {
            products = ProductService.FilterProducts(search.ToLower());
            StateHasChanged();
        }

        public void LoadMore()
        {
            ++page;
            products = ProductService.GetProductsPagination(page);
            StateHasChanged();
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
