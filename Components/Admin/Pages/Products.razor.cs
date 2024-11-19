using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class Products
    {
        /// Sets the class variables to a new instance of the class. This is used to create the list
        public List<ProductModel> products = new List<ProductModel>();
        int page = 1;

        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        /// Called when [ initialized ]. Initializes the instance by getting the list of
        protected override async void OnInitialized()
        {
            base.OnInitialized();

            var company = await SessionService.Get<int>("company");
            products = ProductService.GetAllProductsByCompanyId(company);
        }

        public async void SearchProducts(dynamic args)
        {
            var company = await SessionService.Get<int>("company");
            products = ProductService.SearchProductsByCompanyId(args, company);
        }

        /// Opens the update product modal. Used to update a product's name or description
        /// 
        /// @param ProdId - The id of the
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
                products = ProductService.GetAllProductsByCompanyId(company);
                StateHasChanged();
            }
        }
    }
}
