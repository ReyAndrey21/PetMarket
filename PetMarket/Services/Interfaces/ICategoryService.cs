using PetMarket.Models;
using System.Threading.Tasks; 

namespace PetMarket.Services.Interfaces
{
    public interface ICategoryService
    {

        
        Task<IEnumerable<Category>> GetCategoriesForListAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category?> GetCategoryForEditOrDeleteAsync(int id);

        Task<bool> CreateCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);

        Task<IEnumerable<Category>> GetParentCategoriesAsync();
        Task<IEnumerable<Category>> GetTopLevelCategoriesForCustomerAsync();
        Task<Category?> GetCategoryWithSubcategoriesAsync(int id);
    }
}