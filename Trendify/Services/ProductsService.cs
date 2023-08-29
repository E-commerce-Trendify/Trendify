using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models;

namespace Trendify.Services
{
    public class ProductsService : IProducts
    {
        private readonly EcommerceDbContext _context;
        public ProductsService(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task Create(ProductsDto products)
        {
            var product = new Product()
            {
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                CategoryID = products.CategoryID,
            };
              await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var products = await _context.Products.Where(p => p.ProductID == id).FirstOrDefaultAsync();

            _context.Remove(products);
            await _context.SaveChangesAsync();

        }

        public async Task<List<ProductsDtoView>> GetAllProducts()
        {
            var products = await _context.Products.Include(C=>C.Category).Select(x=> new ProductsDtoView
            {
                ProductID = x.ProductID,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                CategoryID = x.CategoryID,
                Category =x.Category

            }).ToListAsync(); 

            return products;
            
        }

        public async Task<ProductsDtoView> GetProductById(int id)
        {
            var products = await _context.Products.Where(p=>p.ProductID==id).Include(C => C.Category).Select(x => new ProductsDtoView
            {
                ProductID = x.ProductID,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                StockQuantity = x.StockQuantity,
                CategoryID = x.CategoryID,
                Category = x.Category

            }).FirstOrDefaultAsync();
            return products;
        }

        public async Task Update(ProductsDto products , int id)
        {

            var productsUpdate = await _context.Products.Where(p => p.ProductID == id).FirstOrDefaultAsync();

            productsUpdate.Name = products.Name;
            productsUpdate.Description = products.Description;
            productsUpdate.Price = products.Price;
            productsUpdate.CategoryID = products.CategoryID;
              
                await _context.SaveChangesAsync();
            
        }
    }
}
