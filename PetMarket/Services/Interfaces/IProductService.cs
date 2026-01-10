using Microsoft.AspNetCore.Http;
using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? searchTerm = null);
        Task<Product?> GetProductDetailsAsync(int productId);
        Task<bool> CreateProductAsync(Product product, IFormFile? imageFile);
        Task<bool> UpdateProductAsync(Product updatedProduct, IFormFile? newImageFile);
        Task<bool> DeleteProductAsync(int productId);
        Task<IEnumerable<Category>> GetAvailableCategoriesAsync();

        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
    }
}