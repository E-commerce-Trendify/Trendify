using Trendify.DTOs;

namespace Trendify.Interface
{
    public interface ICategory
    {
        // Create
        Task Create(CategoryDTO category);

        // GET All
        Task<List<CategoryDtoView>> GetAllCategories();

        // GET by ID
        Task<CategoryDtoView> GetCategoryById(int id);

        // UPDATE
        Task Update(int id, CategoryDTO category);

        // DELET by ID
        Task Delete(int id);
    }
}
