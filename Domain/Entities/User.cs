
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(Email), Name = "IX_RefreshTokens_Email", IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string NormalizedUserName { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public bool EmailConfirmed { get; set; } = false;

        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;

        public Guid? CreatedBy { get; set; }
    }
}
