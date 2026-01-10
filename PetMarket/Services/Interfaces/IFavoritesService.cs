using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface IFavoritesService
    {
        Task<bool> ToggleFavoriteAsync(string userId, int productId);
        Task<IEnumerable<Favorites>> GetUserFavoritesAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, int productId);
    }
}