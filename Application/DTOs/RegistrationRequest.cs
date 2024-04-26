using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RegistrationRequest
    {
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; } = string.Empty;

        [EmailAddress]
        public required string Email { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters long")]
        public required string Password { get; set; } = string.Empty;

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; } = string.Empty;
    }
}
