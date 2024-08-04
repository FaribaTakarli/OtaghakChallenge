using Microsoft.EntityFrameworkCore.Storage;

namespace OtaghakChallenge.Persistence;

public interface IUnitOfWork
{
    IDbContextTransaction BeginTransaction();

    Task<IDbContextTransaction> BeginTransactionAsync();

    void CommitTransaction();

    Task CommitTransactionAsync();

    void RollbackTransaction();

    Task RollbackTransactionAsync();

    Task<bool> CompletedAsync(CancellationToken ct = default, int count = 0);
}