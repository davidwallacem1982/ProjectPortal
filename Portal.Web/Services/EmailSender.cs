using Microsoft.Extensions.Options;
using Portal.Web.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Portal.Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }

        public EmailSettings EmailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(email, subject, message).Wait();
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? EmailSettings.ToEmail : email.Trim();

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(EmailSettings.UsernameEmail, "Portal Trade Vale")
                };

                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using SmtpClient smtp = new SmtpClient(EmailSettings.PrimaryDomain, EmailSettings.PrimaryPort)
                {
                    EnableSsl = false,
                    Credentials = new NetworkCredential(EmailSettings.UsernameEmail, EmailSettings.UsernamePassword)
                };
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
