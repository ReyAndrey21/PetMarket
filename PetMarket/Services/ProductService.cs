using Microsoft.AspNetCore.Http;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;
using System.Threading.Tasks;

namespace PetMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _fileService = fileService;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? searchTerm = null) =>
            await _productRepository.GetProductsWithCategoryAsync(searchTerm);

        public async Task<Product?> GetProductDetailsAsync(int productId) =>
            await _productRepository.GetProductWithCategoryByIdAsync(productId);

        public async Task<bool> CreateProductAsync(Product product, IFormFile? imageFile)
        {
            
            if (imageFile != null)
            {
                
                product.ImageProductUrl = await _fileService.SaveFileAsync(imageFile, "products");
            }

            await _productRepository.AddAsync(product);
            return await _productRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product updatedProduct, IFormFile? newImageFile)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updatedProduct.IdProduct);
            if (existingProduct == null) return false;

            existingProduct.NameProduct = updatedProduct.NameProduct;
            existingProduct.DescriptionProduct = updatedProduct.DescriptionProduct;
            existingProduct.PriceProduct = updatedProduct.PriceProduct;
            existingProduct.StockProduct = updatedProduct.StockProduct;
            existingProduct.IdCategory = updatedProduct.IdCategory;

            if (newImageFile != null)
            {
                if (!string.IsNullOrEmpty(existingProduct.ImageProductUrl))
                {
                    _fileService.DeleteFile(existingProduct.ImageProductUrl);
                }
        
                existingProduct.ImageProductUrl = await _fileService.SaveFileAsync(newImageFile, "products");
            }

            
            _productRepository.Update(existingProduct);
            return await _productRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return false;

            if (!string.IsNullOrEmpty(product.ImageProductUrl))
            {
                _fileService.DeleteFile(product.ImageProductUrl);
            }

            
            _productRepository.Remove(product);
            return await _productRepository.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Category>> GetAvailableCategoriesAsync()
        {

            return await _categoryRepository.GetLeafCategoriesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        }
    }
}