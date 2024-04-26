using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenByRefreshTokenHashAsync(string refreshTokenHash);
    }
}
