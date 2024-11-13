using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MecuryProduct.Modals
{
    public partial class CartModal
    {
        public List<ProductModel> products = new List<ProductModel>();
        public double subTotal = 0;
        public double grandTotal = 0;

        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            products = await SessionService.Get<List<ProductModel>>("cart");

            if (products is not null && products.Count() > 0)
            {
                foreach (var product in products)
                {
                    subTotal += product.incartquantity * product.regular_price;
                    grandTotal += (product.incartquantity * product.regular_price) + product.vat;
                }
            }
        }

        public async void UpdateCart()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            await SessionService.Set("cart", JsonSerializer.Serialize(products, options));

            products = await SessionService.Get<List<ProductModel>>("cart");
            subTotal = 0;
            grandTotal = 0;

            foreach (var product in products)
            {
                subTotal += product.incartquantity * product.regular_price;
                grandTotal += (product.incartquantity * product.regular_price) + product.vat;
            }

            StateHasChanged();
        }

        public async void clearCart()
        {
            await SessionService.Clear("cart");
            StateHasChanged();
            dialogService.Close();
        }

        public async void checkout()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            List<PaymentProductModel> paymnent_products = new List<PaymentProductModel>();
            var cart_products = await SessionService.Get<List<ProductModel>>("cart");
            await SessionService.Set("payment", JsonSerializer.Serialize(cart_products, options));
            clearCart();
            //await DialogService.OpenAsync<PaymentModal>("Add Payment",
            //    new Dictionary<string, object>() {},
            //    new DialogOptions() { Width = "70%", Height = "90%", Resizable = true, Draggable = true }
            //);
            StateHasChanged();
        }

        public async void RemoveFromCart(ProductModel product)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to remove this product from cart?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                products.Remove(product);
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                await SessionService.Set("cart", JsonSerializer.Serialize(products, options));
                StateHasChanged();
            }
        }
    }
}
