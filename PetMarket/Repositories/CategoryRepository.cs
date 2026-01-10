using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;

namespace PetMarket.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(PetMarketDbContext context) : base(context) { }

        
        public async Task<IEnumerable<Category>> GetAllCategoriesHierarchyAsync()
        {
            return await _dbSet
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.Subcategories)
                    .ThenInclude(sub => sub.Subcategories)
                .OrderBy(c => c.NameCategory)
                .ToListAsync();
        }

        
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.IdCategory == id);
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            return await _dbSet
                .OrderBy(c => c.NameCategory)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetTopLevelCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.ParentCategoryId == null)
                .Include(c => c.Subcategories)
                    .ThenInclude(sub => sub.Subcategories) 
                .ToListAsync();
        }


        
        public async Task<Category?> GetCategoryWithSubcategoriesAsync(int id)
        {
            return await _dbSet
                .Include(c => c.Subcategories)
                    .ThenInclude(sub => sub.Subcategories)
                .FirstOrDefaultAsync(c => c.IdCategory == id);
        }


        public async Task<IEnumerable<Category>> GetLeafCategoriesAsync()
        {

            var nonLeafCategoryIds = await _dbSet

                .Where(c => c.ParentCategoryId.HasValue)
                .Select(c => c.ParentCategoryId.Value)
                .Distinct()
                .ToListAsync();


            return await _dbSet
                .Where(c => !nonLeafCategoryIds.Contains(c.IdCategory))
                .ToListAsync(); 
        }


    }
}