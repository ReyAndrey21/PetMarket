using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;

namespace PetMarket.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(PetMarketDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync(string? searchTerm = null)
        {
            var query = _dbSet.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                
                query = query.Where(p =>
                    p.NameProduct.ToLower().Contains(searchTerm.ToLower())
                );
            }

            return await query.ToListAsync();
        }

        public async Task<Product?> GetProductWithCategoryByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Include(p => p.Review)      
                    .ThenInclude(r => r.User) 
                .FirstOrDefaultAsync(p => p.IdProduct == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.IdCategory == categoryId) 
                .ToListAsync();
        }

    }
}