using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Persistence;

namespace OtaghakChallenge.Persistence
{
    public static class ContextFactory
    {
        public static ApplicationDbContext GetApplicationDbContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("server=.;Database=ProductStore;Trusted_Connection=True;TrustServerCertificate=True;"

              // global setting split query
              //  , c => c.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)

              );
            return new ApplicationDbContext(builder.Options);
        }
    }
}
