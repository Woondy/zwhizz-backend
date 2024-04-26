using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(User user, string resetToken);
        Task SendPasswordResetConfirmationEmail(User user);
    }
}
