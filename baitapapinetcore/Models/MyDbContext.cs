using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace baitapapinetcore.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext (DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherForAcc> VoucherForAccs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Bỏ qua hoặc ghi log thông báo NavigationBaseIncludeIgnored
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));

             optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
