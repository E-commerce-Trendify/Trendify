using Trendify.DTOs;

namespace Trendify.Interface
{
    public interface IProducts
    {
        // Create
        Task Create(ProductsDto products);

        // GET All
        Task<List<ProductsDtoView>> GetAllProducts();

        // GET by ID
        Task<ProductsDtoView> GetProductById(int id);

        // UPDATE
        Task Update(ProductsDto products , int id);

        // DELET by ID
        Task Delete(int id);
    }
}

