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
        }

        public void ChangeQuantity(int prodId, bool decriment)
        {
            if (decriment)
            {
                if ((products.Find(x => x.Id == prodId).incartquantity + 1) > 1)
                {
                    products.Find(x => x.Id == prodId).incartquantity -= 1;
                    ProductService.UpdateProduct(products.Find(x => x.Id == prodId));
                    ProductModel product = products.Find(x => x.Id == prodId);
                    ProductService.UpdateProduct(product);
                }
            }
            else
            {
                var cart_product = products.Find(x => x.Id == prodId);
                if ((cart_product.incartquantity + 1) < cart_product.quantity)
                {
                    products.Find(x => x.Id == prodId).incartquantity += 1;
                    ProductModel product = products.Find(x => x.Id == prodId);
                    ProductService.UpdateProduct(product);
                }
            }
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
