using PetMarket.Models;

namespace PetMarket.Repositories.Interfaces
{
    public interface IFavoritesRepository : IRepository<Favorites>
    {
        Task<IEnumerable<Favorites>> GetByUserIdAsync(string userId);
        Task<Favorites?> GetByUserAndProductAsync(string userId, int productId);
    }
}