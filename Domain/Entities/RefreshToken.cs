using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(UserId), Name = "IX_RefreshTokens_UserId")]
    [Index(nameof(RefreshTokenHash), Name = "IX_RefreshTokens_RefreshTokenHash")]
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string RefreshTokenHash { get; set; }
        public DateTime Expiration { get; set; }

        public virtual User? User { get; set; }  
    }
}
