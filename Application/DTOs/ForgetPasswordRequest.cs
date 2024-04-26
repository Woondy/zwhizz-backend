using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ForgetPasswordRequest
    {
        [EmailAddress]
        public required string Email { get; set; }
    }
}
