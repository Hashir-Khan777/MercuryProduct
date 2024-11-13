using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace MecuryProduct.Modals
{
    public partial class AddExpenseModal
    {
        [Parameter] public string created_by { get; set; }
        public ExpenseModel expense = new ExpenseModel();
        public string file_name = string.Empty;
        public List<CustomerModel> customers = new List<CustomerModel>();
        public List<CompanyModel> companies = new List<CompanyModel>();
        public List<string> expense_types = new List<string>
        {
            "car_buy",
            "stationary",
            "extra"
        };
        public List<string> payment_types = new List<string>
        {
            "card",
            "cash",
            "check"
        };

        [Inject]
        private ExpenseService ExpenseService { get; set; }
        [Inject]
        private CustomerService CustomerService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }

        protected override async void OnInitialized()
        {
            base.OnInitialized();

            var company = await SessionService.Get<int>("company");
            customers = CustomerService.GetCustomersByCompanyId(company);
            companies = CompanyService.GetCompanies();
            var response = await SessionService.Get<ExpenseModel>("expense_form");
            if (response is not null)
            {
                expense = response;
            }
        }

        public async void CraeteExpense()
        {
            expense.company_id = await SessionService.Get<int>("company");
            expense.created_at = DateTime.UtcNow;
            expense.updated_at = DateTime.UtcNow;
            expense.created_by_id = created_by;
            ExpenseService.AddExpense(expense);
            dialogService.Close();
        }

        public async void SetInSession()
        {
            await SessionService.Set("expense_form", JsonSerializer.Serialize(expense));
        }

        public async void changeFile(string? base64)
        {
            if (base64 is not null)
            {
                string directory = Directory.GetCurrentDirectory();
                var datetime = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");
                string filePath = $"{directory}/wwwroot/uploads/" + $"expense-{datetime}-{file_name}";
                int startingIndex = base64.IndexOf(";base64,") + 8;
                string fileBase64 = base64.Substring(startingIndex);
                byte[] file = Convert.FromBase64String(fileBase64);
                System.IO.File.WriteAllBytes(filePath, file);
                expense.reciept_image = "uploads/" + $"expense-{datetime}-{file_name}";
            }
        }
    }
}
