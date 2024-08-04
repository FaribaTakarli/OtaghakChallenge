
using OtaghakChallenge.Domain;
using System.Linq.Expressions;
using System.Numerics;

namespace OtaghakChallenge.Application.Repository;

public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : INumber<TKey>
{
    ValueTask AddAsync(TEntity entity, CancellationToken cancelationToken = default);



    Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> expression = null,
        Expression<Func<TEntity, TResult>> selector = null,
        Expression<Func<TEntity, object>> keySelector = null,
        bool isDesc = true,
        bool asNoTrack = false,
        CancellationToken cancellationToken = default);

      Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression,
     Expression<Func<TEntity, object>> keySelector = null,
     bool isDesc = true,
     bool asNoTrack = false,
     CancellationToken cancellationToken = default);

    Task<TResult> GetSingleAsync<TResult>(Expression<Func<TEntity, bool>> expression,
          Expression<Func<TEntity, TResult>> selector = null,
          Expression<Func<TEntity, object>> keySelector = null,
          bool isDesc = true,
          bool asNoTrack = false,
          CancellationToken cancellationToken = default);




}