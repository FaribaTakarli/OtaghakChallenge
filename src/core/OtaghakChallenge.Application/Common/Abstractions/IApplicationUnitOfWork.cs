using Microsoft.EntityFrameworkCore.Storage;

namespace OtaghakChallenge.Application.Repository;

public interface IApplicationUnitOfWork
{
    IProductRepository Products { get; }

    IDbContextTransaction BeginTransaction();

    Task<IDbContextTransaction> BeginTransactionAsync();

    void CommitTransaction();

    Task CommitTransactionAsync();

    Task<bool> CompletedAsync(CancellationToken cancellationToken = default, int count = 0);

    void RollbackTransaction();

    Task RollbackTransactionAsync();
}