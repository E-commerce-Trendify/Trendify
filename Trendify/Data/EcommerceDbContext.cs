using Microsoft.EntityFrameworkCore;
using Trendify.Models;

namespace Trendify.Data
{
    public class EcommerceDbContext : DbContext
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
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
