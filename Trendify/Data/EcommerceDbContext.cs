using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trendify.Models;
using Trendify.Models.Entites;

namespace Trendify.Data
{
    public class EcommerceDbContext : IdentityDbContext<AuthUser>
    {


        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base method to apply default configurations
            base.OnModelCreating(modelBuilder);

            // Seed data for categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, Name = "Electronics", Description = "Electronic gadgets and devices" },
                new Category { CategoryID = 2, Name = "Clothing", Description = "Fashionable clothing items" }
            );

            // Seed data for products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99m, StockQuantity = 50, CategoryID = 1 },
                new Product { ProductID = 2, Name = "Smartphone", Description = "Latest smartphone model", Price = 699.99m, StockQuantity = 100, CategoryID = 1 }
            );

            //        modelBuilder.Entity<Order>().HasData(
            //    new Order { OrderID = 1, CustomerName = "John Doe", ShippingAddress = "123 Main St", OrderDate = DateTime.Now, TotalAmount = 150.00m },
            //    new Order { OrderID = 2, CustomerName = "Jane Smith", ShippingAddress = "456 Elm St", OrderDate = DateTime.Now, TotalAmount = 250.00m }
            //    // ... add more orders here ...
            //);
            //        // Seed data for order items
            //        modelBuilder.Entity<OrderItem>().HasData(
            //            new OrderItem { OrderItemID = 1, OrderID = 1, ProductID = 1, Quantity = 2, UnitPrice = 50.00m },
            //            new OrderItem { OrderItemID = 2, OrderID = 1, ProductID = 2, Quantity = 1, UnitPrice = 100.00m },
            //            new OrderItem { OrderItemID = 3, OrderID = 2, ProductID = 3, Quantity = 3, UnitPrice = 50.00m }
            //        // ... add more order items here ...

            //);

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole { Id = "admin", Name = "Admin", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = Guid.Empty.ToString() }

             );
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

      //  public DbSet<OrderItem> OrdersItems { get; set; }
     //   public DbSet<Order> Orders { get; set; }
    }
}
