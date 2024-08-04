
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Application.Repository;
using OtaghakChallenge.Domain;
using System.Linq.Expressions;
using System.Numerics;
using OtaghakChallenge.Application.Repository;

namespace OtaghakChallenge.Persistence;

public abstract class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : INumber<TKey>
{
    protected IMapper _mapper;
    protected DbContext _context;
    public GenericRepository(DbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }



    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression,
       Expression<Func<TEntity, object>> keySelector = null,
       bool isDesc = true,
       bool asNoTrack = false,
       CancellationToken cancellationToken = default)
    {
        TEntity result = await GetQueryable(expression, keySelector, isDesc, asNoTrack)
            .SingleOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<TResult> GetSingleAsync<TResult>(Expression<Func<TEntity, bool>> expression,
        Expression<Func<TEntity, TResult>> selector = null,
        Expression<Func<TEntity, object>> keySelector = null,
        bool isDesc = true,
        bool asNoTrack = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(expression, keySelector, isDesc, asNoTrack);

        TResult result = selector is null ?
            await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken) :
            await query.Select(selector).SingleOrDefaultAsync(cancellationToken);

        return result;
    }


    public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, bool>> expression = null,
       Expression<Func<TEntity, TResult>> selector = null,
       Expression<Func<TEntity, object>> keySelector = null,
       bool isDesc = true,
       bool asNoTrack = false,
       CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(expression, keySelector, isDesc, asNoTrack);

        List<TResult> result = selector is null ?
            await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken) :
            await query.Select(selector).ToListAsync(cancellationToken: cancellationToken);

        return result;
    }



    public async ValueTask AddAsync(TEntity entity, CancellationToken cancelationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancelationToken);
    }


    private IQueryable<TEntity> GetIncludedDataSet(IEnumerable<string> includeStatements = null)
    {
        if (includeStatements is null || !includeStatements.Any())
            return _context.Set<TEntity>().AsQueryable();

        return _context.Set<TEntity>().AsQueryable().BatchInclude(includeStatements);
    }


    private IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression = null,
        Expression<Func<TEntity, object>> keySelector = null, bool isDesc = true, bool asNoTrack = false)
    {
        IQueryable<TEntity> query = GetIncludedDataSet();

        query = asNoTrack ? query.AsNoTracking() : query;
        query = expression is null ? query : query.Where(expression);

        if (keySelector is not null)
            query = isDesc ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);

        return query;
    }
}