using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Radzen;
using System.Security.Claims;

namespace MecuryProduct.Components.Manager.Pages
{
    public partial class User
    {
        private List<ApplicationUser> users = new List<ApplicationUser>();
        public string user_id = string.Empty;

        [Inject]
        private UserManager<ApplicationUser> UserManager {  get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }

        protected override void OnInitialized()
        {
            SetUserId();
        }

        public string GetUserClaim(string Id)
        {
            var role = UserService.GetUserClaimByUserId(Id);
            return role;
        }

        public async void DeleteUser(ApplicationUser user)
        {
            bool? deleteUser = await DialogService.Confirm("Are you sure?", "Do you want to delete user?", new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

            if (deleteUser != null && deleteUser == true)
            {
                UserService.DeleteUser(user);
                SetUserId();
                StateHasChanged();
            }
        }

        public async void OpenUpdateUserModal(string UserId)
        {
            await DialogService.OpenAsync<UpdateUserModal>("Update User",
                new Dictionary<string, object>() { { "UserId", UserId } },
                new DialogOptions() { Width = "600px", Height = "60%", Resizable = true, Draggable = true }
            );
            SetUserId();
            StateHasChanged();
        }

        public async void UnlockUser(ApplicationUser user)
        {
            await UserManager.SetLockoutEndDateAsync(user, null);
        }

        /// <summary>
        /// Sets the user ID for the customer based on the authenticated user.
        /// </summary>
        /// <remarks>
        /// This method retrieves the authentication state of the user and sets the user ID for the customer
        /// if the user is authenticated. The user ID is obtained from the claim with the type ClaimTypes.NameIdentifier.
        /// </remarks>
        /// <returns>Void</returns>
        public async void SetUserId()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId is not null)
                {
                    var company = await SessionService.Get<int>("company");
                    user_id = userId;
                    users = UserService.GetAllUsersByCompanyId(company);
                }
            }
        }
    }
}
