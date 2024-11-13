using MecuryProduct.Data;
using MecuryProduct.Modals;
using MecuryProduct.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Radzen;

namespace MecuryProduct.Components.Admin.Pages
{
    public partial class User
    {
        private List<ApplicationUser> users = new List<ApplicationUser>();


        [Inject]
        private UserManager<ApplicationUser> UserManager {  get; set; }
        [Inject]
        private UserService UserService { get; set; }
        [Inject]
        private DialogService DialogService { get; set; }
        [Inject]
        private SessionService SessionService { get; set; }
        [Inject]
        private CompanyService CompanyService { get; set; }

        protected override async void OnInitialized()
        {
            var all_users = UserService.GetAllUsers();
            foreach (var user in all_users)
            {
                user.Role = GetUserClaim(user.Id);
            }
            users = all_users;
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
                var all_users = UserService.GetAllUsers();
                foreach (var obj in all_users)
                {
                    obj.Role = GetUserClaim(obj.Id);
                }
                users = all_users;
                StateHasChanged();
            }
        }

        public async void OpenUpdateUserModal(string UserId)
        {
            await DialogService.OpenAsync<UpdateUserModal>("Update User",
                new Dictionary<string, object>() { { "UserId", UserId } },
                new DialogOptions() { Width = "600px", Height = "60%", Resizable = true, Draggable = true }
            );
            StateHasChanged();
        }

        public async void UnlockUser(ApplicationUser user)
        {
            await UserManager.SetLockoutEndDateAsync(user, null);
        }
    }
}
