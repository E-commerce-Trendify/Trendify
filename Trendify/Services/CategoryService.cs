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
        public CategoryService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task Create(CategoryDTO category)
        {
            Category newTrip = new Category()
            {
                Name = category.Name,
                Description = category.Description
            
            };

            _context.Add(category);
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
                Products = x.Products.ToList()

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
                Products = x.Products.ToList()

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
