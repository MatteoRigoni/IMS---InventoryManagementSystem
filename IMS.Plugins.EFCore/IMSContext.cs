using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductId, pi.InventoryId });

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(i => i.ProductInventories)
                .HasForeignKey(pi => pi.InventoryId);

            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, Name = "Graphic card", Price = 150, Quantity = 1 },
                new Inventory { Id = 2, Name = "CPU", Price = 249, Quantity = 3 },
                new Inventory { Id = 3, Name = "SSD", Price = 100, Quantity = 2 },
                new Inventory { Id = 4, Name = "Motherboard", Price = 70, Quantity = 10 },
                new Inventory { Id = 5, Name = "Case", Price = 15, Quantity = 20 }
            );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name = "Laptop", Price = 600 },
               new Product { Id = 2, Name = "Personal computer", Price = 500 }
           );

            modelBuilder.Entity<ProductInventory>().HasData(
               new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity=1 },
               new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 2, InventoryId = 1, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 2, InventoryId = 2, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 2, InventoryId = 3, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 2, InventoryId = 4, InventoryQuantity = 1 },
               new ProductInventory { ProductId = 2, InventoryId = 5, InventoryQuantity = 1 }
           );
        }
    }
}
