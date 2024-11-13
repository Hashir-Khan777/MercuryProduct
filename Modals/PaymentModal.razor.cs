using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;
using System.Text.Json;
using Radzen;
using MecuryProduct.Components.Admin.Pages;

namespace MecuryProduct.Modals
{
    public partial class PaymentModal
    {
        public List<PosCustomerModel> customers = new List<PosCustomerModel>();
        public List<ProductModel> products = new List<ProductModel>();
        public List<CompanyModel> companies = new List<CompanyModel>();
        public PaymentModel payment = new PaymentModel();
        public LocalizationModel localization = new LocalizationModel();
        public List<string> payment_types = new List<string>
        {
            "Cash",
            "Card",
            "Mobile Banking",
        };
        public double itemsAmount = 0;
        public double taxAmount = 0;
        public double discountAmount = 0;
        public double totalAmount = 0;

        [Inject]
        public SessionService SessionService { get; set; }
        [Inject]
        public CustomerService CustomerService { get; set; }
        [Inject]
        public PaymentService PaymentService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public CompanyService CompanyService { get; set; }
        [Inject]
        public LocalizationService LocalizationService { get; set; }
        [Inject]
        public PosCustomerService PosCustomerService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            companies = CompanyService.GetCompanies();
            products = await SessionService.Get<List<ProductModel>>("payment");

            if (products is not null && products.Count() > 0)
            {
                localization = GetLocalization(products[0].company_id);
            }

            foreach (var product in products)
            {
                double showPrice = (double)product.GetType().GetProperty(localization.showPrice)?.GetValue(product, null);
                var price = product.special ? product.special_price : showPrice;
                itemsAmount += (price * product.incartquantity) - ((price * product.incartquantity) / 100 * product.discount ?? 0);
                taxAmount = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;

                product.tax_1_label = localization.tax_1_label;
                product.tax_1_value = localization.tax_1_value;
                product.tax_2_label = localization.tax_2_label;
                product.tax_2_value = localization.tax_2_value;
                product.tax_3_label = localization.tax_3_label;
                product.tax_3_value = localization.tax_3_value;
                product.tax_4_label = localization.tax_4_label;
                product.tax_4_value = localization.tax_4_value;

                product.show_price = localization.showPrice;
                product.totalTax = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;
                discountAmount += product.discount ?? 0;
                totalAmount += GetAmount(product) - ((GetAmount(product) * payment.cartDiscount) / 100);
            }
            if (payment.paidAmount > totalAmount)
            {
                payment.changeAmount = payment.paidAmount - (int)totalAmount;
            }
            else
            {
                payment.changeAmount = 0;
            }
            var company = await SessionService.Get<int>("company");
            customers = PosCustomerService.GetCustomersByCompanyId(company);
        }

        public LocalizationModel GetLocalization(int? companyId)
        {
            return LocalizationService.GetLocalizationByCompanyId(companyId);
        }

        public double GetPrice(ProductModel product)
        {
            return (double)product.GetType().GetProperty(GetLocalization(product.company_id).showPrice)?.GetValue(product, null);
        }

        public async void GeneratePDF()
        {
            AddPayment();
            await DialogService.OpenAsync<PDFModal>("Save PDF",
                new Dictionary<string, object>() { { "PaymentId", payment.Id } },
                new DialogOptions() { Width = "70%", Height = "90%", Resizable = true, Draggable = true }
            );
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

        public async void AddCustomer()
        {
            await DialogService.OpenAsync<AddPosCustomer>("Add Customer In POS",
                new Dictionary<string, object>() { },
                new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
            );
            var company = await SessionService.Get<int>("company");
            customers = PosCustomerService.GetCustomersByCompanyId(company);
            StateHasChanged();
        }

        public async void AddPayment()
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
            payment.company_id = await SessionService.Get<int>("company");
            payment.created_at = DateTime.UtcNow;
            payment.updated_at = DateTime.UtcNow;
            PaymentService.AddPayment(payment);
            dialogService.Close();
        }

        public async void RemoveFromCart(ProductModel prod)
        {
            bool? deleteCustomer = await DialogService.Confirm("Are you sure?", "Do you want to remove this product?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });
            if (deleteCustomer != null && deleteCustomer == true)
            {
                if (products.Count() > 1)
                {
                    products.Remove(prod);
                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };
                    await SessionService.Set("payment", JsonSerializer.Serialize(products, options));
                    products = await SessionService.Get<List<ProductModel>>("payment");
                    itemsAmount = 0;
                    taxAmount = 0;
                    discountAmount = 0;
                    totalAmount = 0;
                    foreach (var product in products)
                    {
                        double showPrice = (double)product.GetType().GetProperty(localization.showPrice)?.GetValue(product, null);
                        var price = product.special ? product.special_price : showPrice;
                        itemsAmount += (price * product.incartquantity) - ((price * product.incartquantity) / 100 * product.discount ?? 0);
                        taxAmount = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;
                        discountAmount += product.discount ?? 0;
                        totalAmount += GetAmount(product) - ((GetAmount(product) * payment.cartDiscount) / 100);
                    }
                    if (payment.paidAmount > totalAmount)
                    {
                        payment.changeAmount = payment.paidAmount - (int)totalAmount;
                    }
                    else
                    {
                        payment.changeAmount = 0;
                    }
                }
                else
                {
                    products.Remove(prod);
                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        WriteIndented = true
                    };
                    await SessionService.Set("payment", JsonSerializer.Serialize(products, options));
                    await SessionService.Set("cart", JsonSerializer.Serialize(products, options));
                    products = await SessionService.Get<List<ProductModel>>("payment");
                    dialogService.Close();
                }
                StateHasChanged();
            }
        }

        public void UpdatePyment()
        {
            itemsAmount = 0;
            taxAmount = 0;
            discountAmount = 0;
            totalAmount = 0;

            foreach (var product in products)
            {
                double showPrice = (double)product.GetType().GetProperty(localization.showPrice)?.GetValue(product, null);
                var price = product.special ? product.special_price : showPrice;
                itemsAmount += (price * product.incartquantity) - ((price * product.incartquantity) / 100 * product.discount ?? 0);
                taxAmount = localization.tax_1_value + localization.tax_2_value + localization.tax_3_value + localization.tax_4_value;
                discountAmount += product.discount ?? 0;
                totalAmount += GetAmount(product) - ((GetAmount(product) * payment.cartDiscount) / 100);
            }
            if (payment.paidAmount > totalAmount)
            {
                payment.changeAmount = payment.paidAmount - (int)totalAmount;
            }
            else
            {
                payment.changeAmount = 0;
            }
            StateHasChanged();
        }
    }
}
