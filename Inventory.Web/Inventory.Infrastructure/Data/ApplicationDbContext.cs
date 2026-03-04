using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ PurchaseOrder (1) -> PurchaseOrderItems (Many)
            modelBuilder.Entity<PurchaseOrderItems>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.PurchaseOrderItems)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade); // delete order => delete items

            // ✅ Product (1) -> PurchaseOrderItems (Many)
            // Prevent multiple cascade paths
            modelBuilder.Entity<PurchaseOrderItems>()
                .HasOne(x => x.Product)
                .WithMany() // if Product doesn't have collection
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            // ✅ Product (1) -> StockTransactions (Many)
            modelBuilder.Entity<StockTransaction>()
                .HasOne(x => x.Product)
                .WithMany(p => p.StockTransactions)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}