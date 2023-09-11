using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Trendify.Data;
using Trendify.DTOs;
using Trendify.Interface;
using Trendify.Models;

namespace Trendify.Services
{
    public class CategoryService : ICategory
    {
        private readonly EcommerceDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoryService(EcommerceDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> UploadFile(IFormFile file)
        {

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

        public async Task Create(CategoryDTO category,string imageUrl)
        {

            Category category1 = new Category()
            {
                Name = category.Name,
                Description = category.Description,
                ImageUrl = imageUrl
              
            };

            _context.Add(category1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Category DeleteCategory = await _context.Categories.FindAsync(id);

            _context.Entry<Category>(DeleteCategory).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryDtoView>> GetAllCategories()
        {
            var category = await _context.Categories.Include(c => c.Products).Select(x => new CategoryDtoView
            {
                CategoryID = x.CategoryID,
                Name = x.Name,
                Description = x.Description,
                Products = x.Products.ToList(),
                NumberProduct = x.NumberProduct,
                ImageURL= x.ImageUrl
            }).ToListAsync();
            return category;
           }

        public async Task<CategoryDtoView> GetCategoryById(int id)
        {
            var category = await _context.Categories.Where(s => s.CategoryID == id).Include(c => c.Products).Select(x => new CategoryDtoView
            {
                CategoryID = x.CategoryID,
                Name = x.Name,
                Description = x.Description,
                Products = x.Products.ToList(),
                NumberProduct = x.NumberProduct,
                ImageURL = x.ImageUrl

            }).FirstOrDefaultAsync();
            return category;
        }

        public async Task Update(int id, CategoryDTO category)
        {
            var updateCategory = await _context.Categories.FindAsync(id);

                updateCategory.Name = category.Name;
                updateCategory.Description = category.Description;
                
                

                _context.Entry(updateCategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            
        }
    }
}
