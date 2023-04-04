

using BusinessEconomyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessEconomyManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessPeriod> BusinessPeriods { get; set; }
        public DbSet<BusinessSaleTransaction> BusinessSaleTransactions { get; set; }
        public DbSet<BusinessExpenseTransaction> BusinessExpenseTransactions { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierCategory> SupplierCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).ToList().ForEach(x => x.DeleteBehavior = DeleteBehavior.NoAction);
        }
    }
}
