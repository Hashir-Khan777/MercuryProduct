using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MecuryProduct.Modals
{
    public partial class PaymentModal
    {
        public List<CustomerModel> customers = new List<CustomerModel>();
        public List<ProductModel> products = new List<ProductModel>();
        public PaymentModel payment = new PaymentModel();
        public List<string> payment_types = new List<string>
        {
            "Cash",
            "Card",
            "Mobile Banking",
        };
        public double itemsAmount = 0;
        public int taxAmount = 0;
        public int discountAmount = 0;
        public double totalAmount = 0;

        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public CustomerService CustomerService { get; set; }
        [Inject]
        public PaymentService PaymentService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            products = await SessionService.Get<List<ProductModel>>("payment");

            foreach (var product in products)
            {
                itemsAmount += product.regular_price * product.incartquantity;
                taxAmount += product.vat;
                discountAmount += product.discount ?? 0;
                totalAmount += ((product.regular_price * product.incartquantity) + product.vat) - (((product.regular_price * product.incartquantity) + product.vat) / 100 * product.discount) ?? 0;
            }

            customers = CustomerService.GetCustomers();
        }

        public void AddPayment()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            payment.products = JsonSerializer.Serialize(products, options);
            payment.itemsAmount = itemsAmount;
            payment.taxAmount = taxAmount;
            payment.discount = discountAmount;
            payment.totalAmount = totalAmount;
            PaymentService.AddPayment(payment);
            dialogService.Close();
        }

        public void UpdatePyment()
        {
            itemsAmount = 0;
            taxAmount = 0;
            discountAmount = 0;
            totalAmount = 0;

            foreach (var product in products)
            {
                itemsAmount += product.regular_price * product.incartquantity;
                taxAmount += product.vat;
                discountAmount += product.discount ?? 0;
                totalAmount += ((product.regular_price * product.incartquantity) + product.vat) - (((product.regular_price * product.incartquantity) + product.vat) / 100 * product.discount) ?? 0;
            }

            StateHasChanged();
        }
    }
}
