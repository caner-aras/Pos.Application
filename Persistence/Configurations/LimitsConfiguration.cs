using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class LimitsConfiguration : IEntityTypeConfiguration<Limits>
    {
        public void Configure(EntityTypeBuilder<Limits> builder)
        {
            builder.HasKey(e => new { e.Id })
            .ForSqlServerIsClustered(false);
        }
    }
}