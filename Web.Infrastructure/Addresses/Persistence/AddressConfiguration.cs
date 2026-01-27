using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Addresses;

namespace Web.Infrastructure.Addresses.Persistence
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.State)
                .HasMaxLength(100);

            builder.Property(a => a.PostalCode)
                .HasMaxLength(20);

            builder.Property(a => a.Country)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.City);
            builder.HasIndex(a => a.Country);
            builder.HasIndex(a => a.PostalCode);
        }
    }

}