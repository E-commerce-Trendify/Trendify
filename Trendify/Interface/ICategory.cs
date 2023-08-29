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
        Task<CategoryDTO> GetCategoryById(int id);

        // UPDATE
        Task Update(CategoryDTO category);

        // DELET by ID
        Task Delete(int id);
    }
}
