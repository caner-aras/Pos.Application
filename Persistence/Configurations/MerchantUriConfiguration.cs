using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MerchantUriConfiguration : IEntityTypeConfiguration<MerchantUri>
    {
        public void Configure(EntityTypeBuilder<MerchantUri> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}