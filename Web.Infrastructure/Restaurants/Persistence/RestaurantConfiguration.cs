using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Restaurants;

public class RestaurantConfiguration
    : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("RestaurantCategories");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(r => r.Description)
               .HasMaxLength(500);

        builder.Property(r => r.userid)
               .IsRequired();

        builder.HasOne(r => r.AppUser)
               .WithMany()
               .HasForeignKey(r => r.userid)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.MenuCategories)
               .WithOne(m => m.restaurantCategory)
               .HasForeignKey(m => m.RestaurantCategoryId);

        builder.Navigation(r => r.MenuCategories)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }

}
