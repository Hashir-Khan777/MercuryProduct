using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace MecuryProduct.Modals
{
    public partial class CashFlowPdf
    {
        [Parameter] public DateTime start_date { get; set; }
        [Parameter] public DateTime end_date { get; set; }

        List<PaymentModel> payments = new List<PaymentModel>();
        public List<CategoryModel> categories = new List<CategoryModel>();
        public List<ExpenseModel> expenses = new List<ExpenseModel>();
        public List<ProductModel> products = new List<ProductModel>();
        //public DateTime start_date = DateTime.Today.Date;
        //public DateTime end_date = DateTime.Today.Date.AddDays(1);
        public LocalizationModel localization = new LocalizationModel();
        public ElementReference ItemToExport;

        [Inject]
        private PaymentService PaymentService { get; set; }
        [Inject]
        public CategoryService CategoryService { get; set; }
        [Inject]
        public ExpenseService ExpenseService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private LocalizationService LocalizationService { get; set; }
        [Inject]
        private IJSRuntime JS { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            categories = CategoryService.GetCategories();
            var company = await SessionService.Get<int>("company");
            localization = LocalizationService.GetLocalizationByCompanyId(company);
            payments = await PaymentService.GetPaymnetsByCompanyIdWithTime(company, start_date, end_date);
            expenses = await ExpenseService.GetExpensesByCompanyIdWithDate(company, start_date, end_date);
        }

        public async void GetCashFlowByDate()
        {
            var company = await SessionService.Get<int>("company");
            payments = await PaymentService.GetPaymnetsByCompanyIdWithTime(company, start_date, end_date);
            expenses = await ExpenseService.GetExpensesByCompanyIdWithDate(company, start_date, end_date);
            StateHasChanged();
        }

        public double GetTotalExpense()
        {
            double total = 0;

            foreach (var expense in expenses)
            {
                total += expense.amount;
            }

            return total;
        }

        public double GetTotalIncome()
        {
            double revenue = 0;

            foreach (var payment in payments)
            {
                revenue += payment.totalAmount;
            }

            return revenue;
        }

        public double GetItemsAmount()
        {
            double revenue = 0;

            foreach (var payment in payments)
            {
                revenue += payment.itemsAmount;
            }

            return revenue;
        }

        public double GetTotalTaxes()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            double taxAmount = 0;

            foreach (var payment in payments)
            {
                var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);

                taxAmount = payment.taxAmount;
            }

            return taxAmount;
        }

        public double GetTotalTaxesInDollars()
        {
            double taxes = 0;

            taxes = (GetItemsAmount() * GetTotalTaxes()) / 100;

            return taxes;
        }

        public double GetNetIncome()
        {
            double income = 0;

            income = GetTotalIncome() - GetTotalTaxesInDollars();

            return income;
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

        public async void save()
        {
            await JS.InvokeVoidAsync("generateExpensePDF", ItemToExport);
            dialogService.Close();
        }

        public double GetRevenueByCategory(int categoryId)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            double revenue = 0;

            foreach (var payment in payments)
            {
                var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
                var categorizedProducts = products?.FindAll(x => x.CategoryId == categoryId);

                foreach (var product in categorizedProducts.FindAll(x => x.returned == false))
                {
                    double showPrice = (double)product.GetType().GetProperty(product.show_price)?.GetValue(product, null);
                    var price = product.special ? product.special_price : showPrice;
                    revenue += GetAmount(product) + ((GetAmount(product) * payment.cartDiscount) / 100);
                }
            }

            return revenue;
        }

        //public double GetTaxByCategory(int categoryId)
        //{
        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve,
        //        WriteIndented = true
        //    };

        //    double taxAmount = 0;
        //    var categorizedProducts = new List<ProductModel>();

        //    foreach (var payment in payments)
        //    {
        //        var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
        //        categorizedProducts.AddRange(products?.FindAll(x => x.CategoryId == categoryId));
        //    }

        //    foreach (var product in categorizedProducts.FindAll(x => x.returned == false))
        //    {
        //        var price = (double)product.GetType().GetProperty(product.show_price).GetValue(product, null);
        //        taxAmount = ((price * (product.taxAmount ?? 0)) / 100) * product.incartquantity;
        //    }

        //    return taxAmount;
        //}

        public double GetAnyTaxByCategory(int categoryId, string tax)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            double taxAmount = 0;
            var categorizedProducts = new List<ProductModel>();

            foreach (var payment in payments)
            {
                var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
                categorizedProducts.AddRange(products?.FindAll(x => x.CategoryId == categoryId));
            }

            foreach (var product in categorizedProducts.FindAll(x => x.returned == false))
            {
                var tax_name = (double)product.GetType().GetProperty(tax).GetValue(product, null);
                var price = (double)product.GetType().GetProperty(product.show_price).GetValue(product, null);
                taxAmount = ((price * tax_name) / 100) * product.incartquantity;
            }

            return taxAmount;
        }

        public int GetItemsSoldByCategory(int categoryId)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var categorizedProducts = new List<ProductModel>();
            var totalProducts = 0;

            foreach (var payment in payments)
            {
                var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
                categorizedProducts.AddRange(products?.FindAll(x => x.CategoryId == categoryId));
            }

            foreach (var product in categorizedProducts)
            {
                totalProducts += product.incartquantity;
            }

            return totalProducts;
        }

        public int GetItemsReturnedByCategory(int categoryId)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var categorizedProducts = new List<ProductModel>();
            var totalProducts = 0;

            foreach (var payment in payments)
            {
                var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
                categorizedProducts.AddRange(products?.FindAll(x => x.CategoryId == categoryId));
            }

            foreach (var product in categorizedProducts.FindAll(x => x.returned == true))
            {
                totalProducts += product.incartquantity;
            }

            return totalProducts;
        }

        public double GetDiscountByCategory(int categoryId)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            double discountAmount = 0;
            var categorizedProducts = new List<ProductModel>();

            foreach (var payment in payments)
            {
                var products = JsonSerializer.Deserialize<List<ProductModel>>(payment.products, options);
                categorizedProducts.AddRange(products?.FindAll(x => x.CategoryId == categoryId));
            }

            foreach (var product in categorizedProducts.FindAll(x => x.returned == false))
            {
                double showPrice = (double)product.GetType().GetProperty(product.show_price)?.GetValue(product, null);
                var price = product.special ? product.special_price : showPrice;
                discountAmount += product.discount ?? 0;
            }

            return discountAmount;
        }
    }
}
