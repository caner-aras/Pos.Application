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


        // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        // {
        //     ChangeTracker.DetectChanges();

        //     foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //     {
        //         if (entry.State == EntityState.Added)
        //         {
        //             entry.Entity.CreatedBy = _currentUserService.GetUserId();
        //             entry.Entity.Created = _dateTime.Now;

        //         }
        //         else if (entry.State == EntityState.Modified)
        //         {
        //             entry.Entity.LastModifiedBy = _currentUserService.GetUserId();
        //             entry.Entity.LastModified = _dateTime.Now;
        //         }
        //     }

        //     return base.SaveChangesAsync(cancellationToken);
        // }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (MerchantContext).Assembly);
        }
    }
}