using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class Products
    {
        public List<ProductModel> products = new List<ProductModel>();

        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GetProducts();
        }

        public async void GetProducts()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    products = ProductService.GetProductsByManagerId(userId).ToList();
                }
            }
        }

        public async void OpenUpdateProductModal(int ProdId)
        {
            await DialogService.OpenAsync<UpdateProductModal>("Update Product",
                new Dictionary<string, object>() { { "ProductId", ProdId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void DeleteProduct(ProductModel product)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to delete product?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                ProductService.DeleteProduct(product);
                products = ProductService.GetProducts();
                StateHasChanged();
            }
        }
    }
}
