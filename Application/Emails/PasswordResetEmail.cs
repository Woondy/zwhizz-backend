using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Emails
{
    public class PasswordResetEmail(IConfiguration configuration) : BaseEmail(configuration)
    {
        public async Task SendPasswordResetEmail(User user, string resetToken)
        {
            var brandName = _configuration["BrandName"];
            var frontendUrl = _configuration["FrontendUrl"];
            var resetLink = $"{frontendUrl}/reset-password?token={resetToken}";

            var body = $@"
            <body>
                <p>Hi {user.UserName},</p>
                <p>We received a request to reset your password for your account on {brandName}.</p>
                <p>To create a new password, please click the link below:</p>
                <p>{resetLink}</p>
                <p>This link will expire in 30 minutes for your security. If you didn't request a password reset, you can safely ignore this email.</p>
                <p>Sincerely,</p>
                <p>The {brandName} Team</p>
            </body>
            ";

            await SendEmailAsync(user.Email!, $"Reset Your Password for {brandName}", body);
        }
    }
}
