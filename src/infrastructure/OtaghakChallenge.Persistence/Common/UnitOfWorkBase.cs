using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OtaghakChallenge.Persistence;

namespace OtaghakChallenge.Persistence;

public class UnitOfWorkBase : IUnitOfWork
{
    protected readonly DbContext _databaseContext;
    protected IDbContextTransaction _dbContextTransaction;

    public UnitOfWorkBase(DbContext databaseContext)
    {
        _dbContextTransaction = null;
        _databaseContext = databaseContext;
    }

    public IDbContextTransaction BeginTransaction()
    {
        _dbContextTransaction =
            _databaseContext.Database.BeginTransaction();

        return _dbContextTransaction;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _dbContextTransaction =
            await _databaseContext.Database.BeginTransactionAsync();

        return _dbContextTransaction;
    }

    public void CommitTransaction()
    {
        _dbContextTransaction?.Commit();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContextTransaction?.CommitAsync();
    }

    public async Task<bool> CompletedAsync(CancellationToken ct = default, int count = 0)
    {
        return await _databaseContext.SaveChangesAsync(ct) >= count;
    }

    public void RollbackTransaction()
    {
        _dbContextTransaction?.Rollback();
    }

    public async Task RollbackTransactionAsync()
    {
        await _dbContextTransaction?.RollbackAsync();
    }
}