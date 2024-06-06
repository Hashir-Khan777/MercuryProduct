using MecuryProduct.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MecuryProduct.Components.Account
{
    // Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
    internal sealed class IdentityNoOpEmailSender : IEmailSender<ApplicationUser>
    {
        private readonly IEmailSender emailSender = new NoOpEmailSender();

        /// <summary>Sends a confirmation email with a link to the specified email address.</summary>
        /// <param name="user">The user for whom the confirmation email is being sent.</param>
        /// <param name="email">The email address to which the confirmation email will be sent.</param>
        /// <param name="confirmationLink">The link for confirming the account.</param>
        /// <returns>A task representing the asynchronous operation of sending the confirmation email.</returns>
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
            emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

        /// <summary>Sends a password reset link to the specified email address.</summary>
        /// <param name="user">The user for whom the password reset link is being sent.</param>
        /// <param name="email">The email address to which the reset link will be sent.</param>
        /// <param name="resetLink">The link for resetting the password.</param>
        /// <returns>A task representing the asynchronous operation of sending the password reset link.</returns>
        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
            emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

        /// <summary>Sends a password reset code to the specified email address.</summary>
        /// <param name="user">The user for whom the password reset code is being sent.</param>
        /// <param name="email">The email address to which the reset code will be sent.</param>
        /// <param name="resetCode">The password reset code to be included in the email.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
            emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
    }
}
