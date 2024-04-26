using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class LoginRequest
    {
        [EmailAddress]
        public required string Email { get; set; } = string.Empty;

        public required string Password { get; set; } = string.Empty;
    }
}
