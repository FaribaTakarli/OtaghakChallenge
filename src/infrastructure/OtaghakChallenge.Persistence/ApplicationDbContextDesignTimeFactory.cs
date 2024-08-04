using Microsoft.EntityFrameworkCore.Design;

namespace OtaghakChallenge.Persistence
{
    public class ApplicationDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args) => ContextFactory.GetApplicationDbContext();

    }
}
