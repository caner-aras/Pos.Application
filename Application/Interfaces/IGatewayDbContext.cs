using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IGatewayDbContext
    {
        DbSet<Clients> Clients { get; set; }
        DbSet<Gateways> Gateways { get; set; }
        DbSet<Limits> Limits { get; set; }
        DbSet<Log> Log { get; set; }
        DbSet<Merchant> Merchant { get; set; }
        DbSet<MerchantUri> MerchantUri { get; set; }
        DbSet<Rates> Rates { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<Transactions> Transactions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}