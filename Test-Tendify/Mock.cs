using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendify.Data;
using Trendify.Models;

namespace Test_Tendify
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly EcommerceDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new EcommerceDbContext(
                new DbContextOptionsBuilder<EcommerceDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }
        protected async Task<Product> CreateAndSaveTestProduct()
        {
            var product = new Product 
            {
                Name = "Test",
                Description = "Test",
                Price = 2,
                StockQuantity = 1,
                ImageUrl = "Test"

            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, product.ProductID);
            return product;
        }
        
        protected async Task<Category> CreateAndSaveTestCategory()
        {
            var category = new Category 
            { 
              Name = "Test",
              Description = "Test",
              ImageUrl = "Test"
            };
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            Assert.NotEqual(0, category.CategoryID);
            return category;
        }
        
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
