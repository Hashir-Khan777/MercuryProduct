using MecuryProduct.Data;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class AddCustomer
    {
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
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override void OnInitialized()
        {
            SetUserId();
        }

        public void CreateCustomer()
        {
            customer.created_at = DateTime.UtcNow;
            customer.updated_at = DateTime.UtcNow;
            customer.contact_prefrence = selected_contact_prefrence.ToList();
            CustomerService.AddCustomer(customer);
            NavigationManager.NavigateTo("/admin/customers");
        }

        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    customer.created_by_id = userId;
                }
            }
        }
    }
}
