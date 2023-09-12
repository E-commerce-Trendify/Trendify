using Trendify.DTOs;

namespace Trendify.Interface
{
    public interface ICategory
    {
        // Create
        Task Create(CategoryDTO category, string imageUrl);
        Task<string> UploadFile(IFormFile file);


        // GET All
        Task<List<CategoryDtoView>> GetAllCategories();

        // GET by ID
        Task<CategoryDtoView> GetCategoryById(int id);

        // UPDATE
        Task Update(int id, CategoryDTO category, string imageUrl);

        // DELET by ID
        Task Delete(int id);
    }
}
