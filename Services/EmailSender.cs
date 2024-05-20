using System.Net.Mail;
using System.Net;
using MecuryProduct.Data;
using Microsoft.AspNetCore.Identity;

namespace MecuryProduct.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        public SmtpClient client = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("sidramursaleen21@gmail.com", "yiyu psuq qhrl ceei")
        };

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
