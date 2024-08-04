
using Microsoft.EntityFrameworkCore;

namespace OtaghakChallenge.Persistence;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> BatchInclude<TEntity>(this IQueryable<TEntity> source, IEnumerable<string> includeStatements) where TEntity : class
    {
        if (includeStatements is null)
            return source;

        foreach (string includeStatement in includeStatements)
        {
            source = source.Include(includeStatement);
        }

        return source;
    }
}