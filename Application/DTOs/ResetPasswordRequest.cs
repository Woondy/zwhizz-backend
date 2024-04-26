using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ResetPasswordRequest
    {
        [EmailAddress]
        public required string Email { get; set; }

        public required string ResetToken { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters long")]
        public required string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }
    }
}
