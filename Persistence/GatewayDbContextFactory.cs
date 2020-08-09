using Microsoft.EntityFrameworkCore;
using Persistence.Infrastructure;

namespace Persistence
{
    public class GatewayDbContextFactory : DesignTimeDbContextFactoryBase<MerchantContext>
    {
        protected override MerchantContext CreateNewInstance(DbContextOptions<MerchantContext> options)
        {
            return new MerchantContext(options);
        }
    }
}