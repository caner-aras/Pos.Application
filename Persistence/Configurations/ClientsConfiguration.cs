using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientsConfiguration : IEntityTypeConfiguration<Clients>
    {
        public void Configure(EntityTypeBuilder<Clients> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.IdentityNumber)
            .IsRequired()
            .HasMaxLength(11);

            builder.Property(a => a.PhoneNumber)
            .IsRequired()
            .HasMaxLength(10);
        }
    }
}