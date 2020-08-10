using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Persistence {
    public class MerchantContext : DbContext, IGatewayDbContext {
        public MerchantContext (DbContextOptions<MerchantContext> options) : base (options) {

        }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Gateways> Gateways { get; set; }
        public virtual DbSet<Limits> Limits { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Merchant> Merchant { get; set; }
        public virtual DbSet<MerchantUri> MerchantUri { get; set; }
        public virtual DbSet<Rates> Rates { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (MerchantContext).Assembly);
        }
    }
}