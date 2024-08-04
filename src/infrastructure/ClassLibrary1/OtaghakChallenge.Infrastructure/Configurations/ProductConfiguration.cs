using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OtaghakChallenge.Domain;


namespace OtaghakChallenge.infrastructure;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "Pro");

        builder.Property(x => x.Name).IsRequired().HasColumnType("NVARCHAR(200)");
        builder.Property(x => x.Description).IsRequired().HasColumnType("NVARCHAR(1000)");

    }
}
