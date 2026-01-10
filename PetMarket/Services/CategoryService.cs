using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;
using System.Threading.Tasks;

namespace PetMarket.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            
        }

        
        public async Task<IEnumerable<Category>> GetCategoriesForListAsync()
        {
            return await _categoryRepository.GetAllCategoriesHierarchyAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        
        public async Task<Category?> GetCategoryForEditOrDeleteAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return await _categoryRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(category.IdCategory);
            if (existingCategory == null) return false;

            existingCategory.NameCategory = category.NameCategory;
            existingCategory.DescriptionCategory = category.DescriptionCategory;
            existingCategory.ParentCategoryId = category.ParentCategoryId;

            _categoryRepository.Update(existingCategory);
            return await _categoryRepository.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return false;

            _categoryRepository.Remove(category);
            return await _categoryRepository.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            return await _categoryRepository.GetParentCategoriesAsync();
        }

        public async Task<IEnumerable<Category>> GetTopLevelCategoriesForCustomerAsync()
        {

            return await _categoryRepository.GetTopLevelCategoriesAsync();
        }


        public async Task<Category?> GetCategoryWithSubcategoriesAsync(int id)
        {

            return await _categoryRepository.GetCategoryWithSubcategoriesAsync(id);
        }
    }
}