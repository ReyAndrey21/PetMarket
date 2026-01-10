using PetMarket.Models;

namespace PetMarket.Repositories.Interfaces
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetCartByUserIdAsync(string userId);
        Task RemoveItemAsync(ShoppingCartItem item);
        Task SaveChangesAsync();
    }
}