using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Domain;
using OtaghakChallenge.Domain.Enums;
using OtaghakChallenge.Domain.Idp;
using OtaghakChallenge.infrastructure;
using System.Reflection;

namespace OtaghakChallenge.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public const String DefaultSchema = "Pro";

        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          //  modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);


            modelBuilder.Entity<Product>().HasQueryFilter(x => x.Status == Status.Active);

            // Define the shadow property for all entities that inherit from BaseEntity  
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity<int>).IsAssignableFrom(entityType.ClrType))
                {
                    // Define a shadow property 
                    entityType.AddProperty("CreatedOn", typeof(DateTime));
                    entityType.AddProperty("UpdateOn", typeof(DateTime?));
                    entityType.AddProperty("CreatedBy", typeof(int));
                    entityType.AddProperty("UpdatedBy", typeof(int?));
                }
            }


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var ModifiedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in ModifiedEntries)
            {

                var propertyUpdateOnInfo = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetProperty("UpdateOn");
                if (propertyUpdateOnInfo != null)
                {
                    entry.Property("UpdateOn").CurrentValue = DateTime.Now;
                }

                var propertyUpdatedByInfo = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetProperty("UpdatedBy");
                if (propertyUpdatedByInfo != null)
                {
                    //get UserId from Token
                    var UserId = 2;
                    entry.Property("UpdatedBy").CurrentValue = UserId;

                }
            }


            var AddedEntries = ChangeTracker.Entries()
               .Where(e => e.State == EntityState.Added);

            foreach (var entry in AddedEntries)
            {

                //var propertyUpdateOnInfo = entry.Entity.GetType().GetProperty("CreatedOn");
                var propertyUpdateOnInfo = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetProperty("CreatedOn");

                if (propertyUpdateOnInfo != null)
                {
                    //  propertyUpdateOnInfo.SetValue(entry.Entity, DateTime.Now);
                    entry.Property("CreatedOn").CurrentValue = DateTime.Now;
                }

                var propertyUpdatedByInfo = entry.Context.Model.FindEntityType(entry.Entity.GetType()).GetProperty("CreatedBy");
                if (propertyUpdatedByInfo != null)
                {
                    //get UserId from Token
                    var UserId = 1;
                    entry.Property("CreatedBy").CurrentValue = UserId;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
