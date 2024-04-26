using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly AppDbContext _appDbContext;
        private bool _disposedValue = false;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            UserRepository = new UserRepository(_appDbContext);
            RefreshTokenRepository = new RefreshTokenRepository(_appDbContext);
        }

        public IUserRepository UserRepository { get; private set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void RollbackChanges()
        {
            _appDbContext.ChangeTracker.Clear();
        }

        public async ValueTask DisposeAsync()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            await _appDbContext.DisposeAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    //
                }

                _disposedValue = true;
            }
        }
    }
}
