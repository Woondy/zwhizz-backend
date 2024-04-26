using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Emails
{
    public class PasswordResetConfirmationEmail(IConfiguration configuration) : BaseEmail(configuration)
    {
        public async Task SendPasswordResetConfirmationEmail(User user)
        {
            var brandName = _configuration["BrandName"];
            var supportEmail = _configuration["SupportEmail"];

            var body = $@"
            <body>
                <p>Hi {user.UserName},</p>
                <p>Your password reset request has been processed successfully. You can now log in to your account with your new password.</p>
                <p>If you did not initiate this password reset, please contact our security team immediately at {supportEmail} to secure your account.</p>
                <p>Sincerely,</p>
                <p>The {brandName} Team</p>
            </body>
            ";

            await SendEmailAsync(user.Email!, "Your Password Reset is Complete", body);
        }
    }
}
