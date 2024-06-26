using Microsoft.EntityFrameworkCore.Storage;

namespace ChatApp.Persistence.DbManager
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _contextTransaction;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateTransaction()
        {
            _contextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _contextTransaction?.Commit();
        }

        public void Rollback()
        {
            _contextTransaction?.Rollback();
            _contextTransaction?.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
