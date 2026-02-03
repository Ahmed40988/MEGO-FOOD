using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.BaseCategories;

namespace Web.Infrastructure.BaseCategories.Persistence
{
    public class BaseCategoryConfiguration
        : IEntityTypeConfiguration<BaseCategory>
    {
        public void Configure(EntityTypeBuilder<BaseCategory> builder)
        {
            builder.ToTable("BaseCategories");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Description)
                   .HasMaxLength(500);

            builder.Property(b => b.UserId)
                   .IsRequired();

            builder.HasMany(b => b.Restaurants)
                   .WithOne(r => r.BaseCatgory)
                   .HasForeignKey(r => r.BaseCatgoryId);

            builder.Navigation(b => b.Restaurants)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

