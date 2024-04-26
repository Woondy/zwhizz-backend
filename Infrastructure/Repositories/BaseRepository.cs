using Domain.Interfaces.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T>(AppDbContext appDbContext) : IRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _appDbContext.FindAsync<T>(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _appDbContext.AddAsync(entity);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _appDbContext.Remove(entity);
        }
    }
}
