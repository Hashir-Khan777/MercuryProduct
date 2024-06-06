using MecuryProduct.Data;
using Microsoft.AspNetCore.Identity;

namespace MecuryProduct.Components.Account
{
    internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
    {
        /// <summary>
        /// Retrieves the ApplicationUser associated with the current HttpContext user.
        /// </summary>
        /// <param name="context">The current HttpContext.</param>
        /// <returns>The ApplicationUser object associated with the current user.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is not found.</exception>
        public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
        {
            var user = await userManager.GetUserAsync(context.User);

            if (user is null)
            {
                redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
            }

            return user;
        }
    }
}
