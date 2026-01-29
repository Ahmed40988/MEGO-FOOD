using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.MenuCategories;

namespace Web.Infrastructure.MenuCategories.Persistence
{
    public class MenuCategoryConfiguration
    : IEntityTypeConfiguration<MenuCategory>
    {
        public void Configure(EntityTypeBuilder<MenuCategory> builder)
        {
            builder.ToTable("MenuCategories");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(m => m.Description)
                   .HasMaxLength(500);

            builder.HasOne(m => m.restaurant)
                   .WithMany(r => r.MenuCategories)
                   .HasForeignKey(m => m.RestaurantId);

            builder.HasMany(m => m.Products)
                   .WithOne(p => p.MenuCategory)
                   .HasForeignKey(p => p.MenuCategoryId);

            builder.Navigation(m => m.Products)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
