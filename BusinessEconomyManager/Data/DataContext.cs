using BusinessEconomyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessEconomyManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ServiceSuppliedType> ServiceSuppliedTypes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierOperation> SupplierOperations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBusiness> UserBusinesses { get; set; }
        public DbSet<UserBusinessPeriod> UserBusinessPeriods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Global turn off delete behaviour on foreign keys
            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).ToList().ForEach(x => x.DeleteBehavior = DeleteBehavior.NoAction);
        }
    }
}
