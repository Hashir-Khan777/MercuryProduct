using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Employee.Pages
{
    public partial class Products
    {
        /// Sets the class variables to a new instance of the class. This is used to create the list
        public List<ProductModel> products = new List<ProductModel>();

        /// Declares the service to be used. This is a singleton so you can't create a new service
        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// Called when the component is initialized. This is where you can perform actions that need to be carried out before the component is initialized
        protected override void OnInitialized()
        {
            base.OnInitialized();

            GetProducts();
        }

        /// Gets list of products for current user. This method is used to show product list
        public async void GetProducts()
        {
            var company = await SessionService.Get<int>("company");
            products = ProductService.GetProductsByCompanyId(company).ToList();
        }

        public async void SearchProducts(dynamic args)
        {
            var company = await SessionService.Get<int>("company");
            products = ProductService.SearchProductsByCompanyId(args, company);
        }

        /// Opens the update product modal. Used to update a product's name or description
        /// 
        /// @param ProdId - The id of the product
        public async void OpenUpdateProductModal(int ProdId)
        {
            await DialogService.OpenAsync<UpdateProductModal>("Update Product",
                new Dictionary<string, object>() { { "ProductId", ProdId } },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        /// Deletes a product from the database. Automatically prompts the user to confirm deletion
        /// 
        /// @param product - Product to be deleted
        public async void DeleteProduct(ProductModel product)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to delete product?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            /// Delete the customer and all products.
            if (deleteCustomer != null && deleteCustomer == true)
            {
                ProductService.DeleteProduct(product);
                var company = await SessionService.Get<int>("company");
                products = ProductService.GetProductsByCompanyId(company);
                StateHasChanged();
            }
        }
    }
}
