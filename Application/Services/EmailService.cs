using Application.Emails;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly PasswordResetEmail _passwordResetEmail = new(configuration);
        private readonly PasswordResetConfirmationEmail _passwordResetConfirmationEmail = new(configuration);

        public async Task SendPasswordResetEmail(User user, string resetToken)
        {
            await _passwordResetEmail.SendPasswordResetEmail(user, resetToken);
        }

        public async Task SendPasswordResetConfirmationEmail(User user)
        {
            await _passwordResetConfirmationEmail.SendPasswordResetConfirmationEmail(user);
        }
    }
}
