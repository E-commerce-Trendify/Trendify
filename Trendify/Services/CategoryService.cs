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

        /// <summary>
        /// Uploads a file to Azure Blob Storage and returns the URL of the uploaded file.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <returns>The URL of the uploaded file.</returns>
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file != null)
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
            return "https://emojigraph.org/media/microsoft/shopping-cart_1f6d2.png";
        }

        /// <summary>
        /// Creates a new category in the database.
        /// </summary>
        /// <param name="category">The category data to create.</param>
        /// <param name="imageUrl">The URL of the category's image.</param>
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

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        public async Task Delete(int id)
        {
            Category DeleteCategory = await _context.Categories.FindAsync(id);

            _context.Entry<Category>(DeleteCategory).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a list of all categories along with their associated products.
        /// </summary>
        /// <returns>A list of CategoryDtoView objects representing categories.</returns>
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

        /// <summary>
        /// Retrieves a category by its ID along with its associated products.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A CategoryDtoView object representing the category.</returns>
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

        /// <summary>
        /// Updates an existing category with new data.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="category">The updated category data.</param>
        /// <param name="ImageURL">The URL of the updated category image.</param>
        public async Task Update(int id, CategoryDTO category, string ImageURL)
        {
            var updateCategory = await _context.Categories.FindAsync(id);

                updateCategory.Name = category.Name;
                updateCategory.Description = category.Description;
            updateCategory.ImageUrl = ImageURL;
                _context.Entry(updateCategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            
        }
    }
}
