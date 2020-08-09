using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class GatewaysConfiguration : IEntityTypeConfiguration<Gateways>
    {
        public void Configure(EntityTypeBuilder<Gateways> builder)
        {
            builder.HasKey(e => new { e.Id })
            .ForSqlServerIsClustered(false);
        }
    }
}