using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;


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

        
        public async Task<bool> CreateProductAsync(Product product, IFormFile? imageFile, IFormFile? pdfFile)
        {
            if (imageFile != null)
            {
                product.ImageProductUrl = await _fileService.SaveFileAsync(imageFile, "products");
            }

            
            if (pdfFile != null)
            {
                product.DescriptionPdfUrl = await _fileService.SaveFileAsync(pdfFile, "documents");
            }

            await _productRepository.AddAsync(product);
            return await _productRepository.SaveChangesAsync() > 0;
        }

        
        public async Task<bool> UpdateProductAsync(Product updatedProduct, IFormFile? newImageFile, IFormFile? newPdfFile)
        {
            var existingProduct = await _productRepository.GetByIdAsync(updatedProduct.IdProduct);
            if (existingProduct == null) return false;

            existingProduct.NameProduct = updatedProduct.NameProduct;
            existingProduct.BrandProduct = updatedProduct.BrandProduct;
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

            
            if (newPdfFile != null)
            {
                
                if (!string.IsNullOrEmpty(existingProduct.DescriptionPdfUrl))
                {
                    _fileService.DeleteFile(existingProduct.DescriptionPdfUrl);
                }
                
                existingProduct.DescriptionPdfUrl = await _fileService.SaveFileAsync(newPdfFile, "documents");
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

           
            if (!string.IsNullOrEmpty(product.DescriptionPdfUrl))
            {
                _fileService.DeleteFile(product.DescriptionPdfUrl);
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