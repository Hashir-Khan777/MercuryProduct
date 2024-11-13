using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;
using Radzen;

namespace MecuryProduct.Modals
{
    public partial class ViewPaymentModal
    {
        [Inject]
        private PaymentService PaymentService { get; set; }
        [Inject]
        public LocalizationService LocalizationService { get; set; }

        [Parameter] public int PaymentId { get; set; }

        public List<ProductModel> products { get; set; }
        public PaymentModel payment { get; set; }

        protected override async void OnInitialized()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            payment = PaymentService.GetPaymentById(PaymentId);
            products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);

            base.OnInitialized();
        }

        public LocalizationModel GetLocalization(int? companyId)
        {
            return LocalizationService.GetLocalizationByCompanyId(companyId);
        }

        public double GetPrice(ProductModel product)
        {
            return (double)product.GetType().GetProperty(GetLocalization(product.company_id).showPrice)?.GetValue(product, null);
        }

        public double GetTax(ProductModel item)
        {
            return (double)(((GetPrice(item) * item.totalTax) / 100) * item.incartquantity);
        }

        public double GetAmount(ProductModel item)
        {
            var key = item.GetType().GetProperty(item.show_price);
            if (key is not null)
            {
                var showPrice = key.GetValue(item, null);
                double price = item.special ? (double)item.special_price : (double)showPrice;
                double tax = (double)item.tax_1_value + (double)item.tax_2_value + (double)item.tax_3_value + (double)item.tax_4_value;
                var totalPrice = (price + ((price * tax) / 100)) * item.incartquantity;
                var discounted_amount = (totalPrice * item.discount ?? 0) / 100;
                return totalPrice - discounted_amount;
            }
            return 0;
        }

        public void RetunProduct(ProductModel item)
        {
            if (products.FindAll(x => x.returned == false).Count() > 1)
                {
                var prod = products.FirstOrDefault(x => x.Id == item.Id);
                if (prod is not null)
                {
                    prod.returned = true;
                    prod.returned_at = DateTime.UtcNow;
                }
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };
                payment.products = JsonSerializer.Serialize(products, options);
                products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
                double itemsAmount = 0;
                double taxAmount = 0;
                double discountAmount = 0;
                double totalAmount = 0;
                foreach (var product in products.FindAll(x => x.returned == false))
                {
                    double showPrice = (double)product.GetType().GetProperty(product.show_price)?.GetValue(product, null);
                    var price = product.special ? product.special_price : showPrice;
                    itemsAmount += (price * product.incartquantity) - ((price * product.incartquantity) / 100 * product.discount) ?? 0;
                    taxAmount += (double)item.totalTax;
                    discountAmount += product.discount ?? 0;
                    totalAmount += GetAmount(product) + ((GetAmount(product) * payment.cartDiscount) / 100);
                }
                if (payment.paidAmount > totalAmount)
                {
                    payment.changeAmount = payment.paidAmount - (int)totalAmount;
                }
                else
                {
                    payment.changeAmount = 0;
                }
                payment.itemsAmount = itemsAmount;
                payment.taxAmount = taxAmount;
                payment.discount = discountAmount;
                payment.totalAmount = totalAmount;
                PaymentService.UpdatePayment(payment);
            }
            else
            {
                PaymentService.DeletePayment(payment);
                dialogService.Close();
            }
        }
    }
}
