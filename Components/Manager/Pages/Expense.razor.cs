using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class Expense
    {
        List<PaymentModel> payments = new List<PaymentModel>();
        public List<CategoryModel> categories = new List<CategoryModel>();
        public List<ExpenseModel> expenses = new List<ExpenseModel>();
        public List<ProductModel> products = new List<ProductModel>();
        public DateTime start_date = DateTime.Today.Date;
        public DateTime end_date = DateTime.Today.Date.AddDays(1);
        public LocalizationModel localization = new LocalizationModel();

        [Inject]
        private PaymentService PaymentService { get; set; }
        [Inject]
        public CategoryService CategoryService { get; set; }
        [Inject]
        public ExpenseService ExpenseService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private LocalizationService LocalizationService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Initialization();
        }

        public async void Initialization()
        {
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

        public async void OpenAddExpenseModal()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    await DialogService.OpenAsync<AddExpenseModal>("Add Expense",
                        new Dictionary<string, object>() { { "created_by", userId } },
                        new DialogOptions() { Width = "700px", Height = "90%", Resizable = true, Draggable = true }
                    );
                    var company = await SessionService.Get<int>("company");
                    expenses = await ExpenseService.GetExpensesByCompanyIdWithDate(company, start_date, end_date);
                    StateHasChanged();
                }
            }
        }

        public async void OpenPrintPDFModal()
        {
            await DialogService.OpenAsync<CashFlowPdf>("Save Cash Flow",
                new Dictionary<string, object>() { { "start_date", start_date }, { "end_date", end_date } },
                new DialogOptions() { Width = "90%", Height = "90%", Resizable = true, Draggable = true }
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
