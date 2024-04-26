using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        Task<string> GenerateRefreshTokenAsync(User user);
        Task DeleteRefreshTokenAsync(RefreshToken refreshToken);
    }
}
