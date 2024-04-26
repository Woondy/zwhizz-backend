using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Application.Emails
{
    public abstract class BaseEmail(IConfiguration configuration)
    {
        protected readonly IConfiguration _configuration = configuration;
        protected string GetFromEmail() => _configuration["Smtp:FromAddress"]!;

        protected async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            using var client = new SmtpClient(_configuration["Smtp:Server"], Convert.ToInt32(_configuration["Smtp:Port"]));
            client.Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            client.EnableSsl = true;

            var message = new MailMessage(GetFromEmail(), recipientEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(message);
        }
    }
}
