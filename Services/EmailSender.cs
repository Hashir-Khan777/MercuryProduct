using System.Net.Mail;
using System.Net;
using MecuryProduct.Data;
using Microsoft.AspNetCore.Identity;

namespace MecuryProduct.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        /* This code snippet is initializing an instance of the `SmtpClient` class named `client`. It sets
        various properties of the `SmtpClient` object such as the host, port, SSL settings, delivery method,
        and credentials required for sending emails via Gmail's SMTP server. */
        public SmtpClient client = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("sidramursaleen21@gmail.com", "yiyu psuq qhrl ceei")
        };

        /// <summary>Sends a confirmation link to a user's email address.</summary>
        /// <param name="user">The user to whom the confirmation link is being sent.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="confirmationLink">The confirmation link to be sent.</param>
        /// <returns>A task representing the asynchronous operation of sending the confirmation link.</returns>
        Task IEmailSender<ApplicationUser>.SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("sidramursaleen21@gmail.com"),
                Subject = "Confirmation Link",
                Body = confirmationLink,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }

        /// <summary>Sends a password reset code to a user via email.</summary>
        /// <param name="user">The user for whom the password reset code is being sent.</param>
        /// <param name="email">The email address to which the reset code will be sent.</param>
        /// <param name="resetCode">The password reset code to be sent.</param>
        /// <returns>A task representing the asynchronous operation of sending the email.</returns>
        Task IEmailSender<ApplicationUser>.SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("sidramursaleen21@gmail.com"),
                Subject = "Reset Code",
                Body = resetCode,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }

        /// <summary>
        /// Sends a password reset link to the specified email address for the given user.
        /// </summary>
        /// <param name="user">The user for whom the password reset link is being sent.</param>
        /// <param name="email">The email address to which the reset link will be sent.</param>
        /// <param name="resetLink">The password reset link to be included in the email.</param>
        /// <returns>A task representing the asynchronous operation of sending the password reset email.</returns>
        Task IEmailSender<ApplicationUser>.SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("sidramursaleen21@gmail.com"),
                Subject = "Reset Link",
                Body = resetLink,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            return client.SendMailAsync(mailMessage);
        }
    }
}
