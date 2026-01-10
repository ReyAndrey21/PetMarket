using PetMarket.Models;

namespace PetMarket.Repositories.Interfaces
{

    public interface ICategoryRepository : IRepository<Category>
    {

        
        Task<IEnumerable<Category>> GetAllCategoriesHierarchyAsync();

        Task<IEnumerable<Category>> GetParentCategoriesAsync();
        Task<IEnumerable<Category>> GetTopLevelCategoriesAsync();
        Task<Category?> GetCategoryWithSubcategoriesAsync(int id);
        Task<IEnumerable<Category>> GetLeafCategoriesAsync();
    }
}