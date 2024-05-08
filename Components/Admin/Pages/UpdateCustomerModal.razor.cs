using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class UpdateCustomerModal
    {
        [Parameter] public int cusId {  get; set; }

        public CustomerModel customer = new CustomerModel();
        public List<string> customer_types = new List<string>()
        {
            "Phone",
            "Web",
            "Walk In",
            "Bulk"
        };
        public List<string> number_types = new List<string>()
        {
            "Cell",
            "Home",
            "Office",
        };
        public IEnumerable<string> contact_prefrence = new string[]
        {
            "Email",
            "Phone",
            "Text"
        };
        public IEnumerable<string> selected_contact_prefrence = new string[] { };
        public string email_regex = "^(([^<>()[\\]\\\\.,;:\\s@\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\"]+)*)|.(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$";

        [Inject]
        private CustomerService CustomerService { get; set; }

        protected override void OnInitialized()
        {
            GetCustomerById();
        }

        public void GetCustomerById()
        {
            var result = CustomerService.GetCustomerById(cusId);
            if (result != null)
            {
                customer = result;
                selected_contact_prefrence = result.contact_prefrence;
            }
        }

        public void UpdateCustomer()
        {
            customer.contact_prefrence = selected_contact_prefrence.ToList();
            customer.updated_at = DateTime.UtcNow;
            CustomerService.UpdateCustomer(customer);
            dialogService.Close();
        }
    }
}
