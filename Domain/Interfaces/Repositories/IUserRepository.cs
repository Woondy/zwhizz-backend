using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByPasswordResetTokenAsync(string resetToken);
    }
}
