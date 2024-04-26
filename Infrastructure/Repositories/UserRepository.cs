using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository(AppDbContext appDbContext) : BaseRepository<User>(appDbContext), IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByPasswordResetTokenAsync(string passwordResetToken)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == passwordResetToken && u.PasswordResetTokenExpiry > DateTime.UtcNow);
        }
    }
}
