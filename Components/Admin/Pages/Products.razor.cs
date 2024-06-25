using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Products
    {
        public List<ProductModel> products = new List<ProductModel>();

        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            products = ProductService.GetProducts();
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
