using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository(AppDbContext appDbContext) : BaseRepository<RefreshToken>(appDbContext), IRefreshTokenRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<RefreshToken?> GetRefreshTokenByRefreshTokenHashAsync(string refreshTokenHash)
        {
            return await _appDbContext.RefreshTokens.Include(i => i.User).FirstOrDefaultAsync(u => u.RefreshTokenHash == refreshTokenHash);
        }
    }
}
