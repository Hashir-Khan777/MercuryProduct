using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json.Serialization;
using System.Text.Json;
using MecuryProduct.Modals;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class CartComponent
    {
        public List<ProductModel> products = new List<ProductModel>();
        public LocalizationModel localization = new LocalizationModel();
        public double subTotal = 0;
        public double grandTotal = 0;
        public double taxTotal = 0;

        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public ProductService ProductService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public LocalizationService LocalizationService { get; set; }


        protected override async void OnInitialized()
        {
            base.OnInitialized();

            products = await SessionService.Get<List<ProductModel>>("cart");

            if (products is not null && products.Count() > 0)
            {
                localization = GetLocalization(products[0].company_id);
                foreach (var product in products)
                {
                    double showPrice = (double)product.GetType().GetProperty(localization.showPrice)?.GetValue(product, null);
                    var price = product.special ? product.special_price : showPrice;
                    var tax = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;
                    taxTotal = tax;
                    subTotal += product.incartquantity * price;
                    grandTotal += (price + ((price * tax) / 100)) * product.incartquantity;
                }
            }
        }

        public LocalizationModel GetLocalization(int? companyId)
        {
            return LocalizationService.GetLocalizationByCompanyId(companyId);
        }

        public double GetPrice(ProductModel product)
        {
            return (double)product.GetType().GetProperty(GetLocalization(product.company_id).showPrice)?.GetValue(product, null);
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
            taxTotal = 0;

            foreach (var product in products)
            {
                double showPrice = (double)product.GetType().GetProperty(localization.showPrice)?.GetValue(product, null);
                var price = product.special ? product.special_price : showPrice;
                var tax = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;
                taxTotal = tax;
                subTotal += product.incartquantity * price;
                grandTotal += (price + ((price * tax) / 100)) * product.incartquantity;
            }

            StateHasChanged();
        }
        public async void clearCart()
        {
            await SessionService.Clear("cart");
            products = new List<ProductModel>();
            StateHasChanged();
        }
        public async void checkout()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var cart_products = await SessionService.Get<List<ProductModel>>("cart");
            await SessionService.Set("payment", JsonSerializer.Serialize(cart_products, options));
            clearCart();
            await DialogService.OpenAsync<PaymentModal>("Add Payment",
                new Dictionary<string, object>() {},
                new DialogOptions() { Width = "70%", Height = "90%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void RemoveFromCart(ProductModel prod)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to remove this product from cart?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                products.Remove(prod);
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                await SessionService.Set("cart", JsonSerializer.Serialize(products, options));
                products = await SessionService.Get<List<ProductModel>>("cart");

                subTotal = 0;
                grandTotal = 0;
                taxTotal = 0;
                if (products is not null && products.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        double showPrice = (double)product.GetType().GetProperty(localization.showPrice)?.GetValue(product, null);
                        var price = product.special ? product.special_price : showPrice;
                        var tax = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;
                        taxTotal = tax;
                        subTotal += product.incartquantity * price;
                        grandTotal += (price + ((price * tax) / 100)) * product.incartquantity;
                    }
                }
                StateHasChanged();
            }
        }
    }
}
