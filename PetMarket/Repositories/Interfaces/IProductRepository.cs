using PetMarket.Models;

namespace PetMarket.Repositories.Interfaces
{
    
    public interface IProductRepository : IRepository<Product>
    {
        
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync(string? searchTerm = null);
        Task<Product?> GetProductWithCategoryByIdAsync(int id);

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }
}