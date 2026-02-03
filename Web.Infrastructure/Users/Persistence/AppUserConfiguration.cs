using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Web.Infrastructure.Users.Persistence
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder
             .OwnsMany(x => x.RefreshTokens)
             .ToTable("RefreshTokens")
             .WithOwner()
             .HasForeignKey("UserId");





        }
    }
}
