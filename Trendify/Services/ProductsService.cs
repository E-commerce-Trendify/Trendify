using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
        private readonly IConfiguration _configuration;
        public ProductsService(EcommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> UploadFile(IFormFile file)
        {

            if(file !=null) {
                BlobContainerClient blobcontener = new BlobContainerClient(_configuration.GetConnectionString("StorageAuzer"), "images");
                await blobcontener.CreateIfNotExistsAsync();
                BlobClient blobClient = blobcontener.GetBlobClient(file.FileName);
                using var fileStream = file.OpenReadStream();
                BlobUploadOptions blobOption = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
                };
                if (!blobClient.Exists())
                {
                    await blobClient.UploadAsync(fileStream, blobOption);
                }
                return blobClient.Uri.ToString();
            }
            return "https://emojigraph.org/media/microsoft/shopping-cart_1f6d2.png";
        }

        public async Task Create(ProductsDto products, string imageurl)
        {



            var product = new Product()
            {
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                CategoryID = products.CategoryID,
                ImageUrl = imageurl
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
                Category =x.Category,
                ImageUrl = x.ImageUrl

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
                Category = x.Category,
                ImageUrl =x.ImageUrl
            }).FirstOrDefaultAsync();
            return products;
        }

        public async Task Update(ProductsDto products , int id, string imageurl)
        {

            var productsUpdate = await _context.Products.Where(p => p.ProductID == id).FirstOrDefaultAsync();

            productsUpdate.Name = products.Name;
            productsUpdate.Description = products.Description;
            productsUpdate.Price = products.Price;
            productsUpdate.StockQuantity = products.StockQuantity;
            productsUpdate.CategoryID = products.CategoryID;
            productsUpdate.ImageUrl = imageurl;
            
            await _context.SaveChangesAsync();
            
        }
    }
}
