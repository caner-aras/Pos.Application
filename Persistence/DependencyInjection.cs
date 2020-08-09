using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence {
    public static class DependencyInjection {
        public static IServiceCollection AddPersistence (this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<IGatewayDbContext, MerchantContext> (options =>
                options.UseSqlServer (configuration.GetConnectionString ("GatewayConnection")));

            return services;
        }
    }
}