using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace Web.Infrastructure.Products.Persistence
{
    public class ProductConfiguration
    : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasMaxLength(500);

            builder.Property(x => x.ImagesURL)
    .HasConversion(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.Rating)
                   .HasDefaultValue(0);

            builder.HasOne(p => p.MenuCategory)
                   .WithMany(m => m.Products)
                   .HasForeignKey(p => p.MenuCategoryId);
        }
    }
}
