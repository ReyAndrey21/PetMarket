using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;

namespace PetMarket.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly PetMarketDbContext _context;

        public ShoppingCartRepository(PetMarketDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetCartByUserIdAsync(string userId)
        {
            
            return await _context.ShoppingCarts
                .Include(c => c.ShoppingCartItem)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task RemoveItemAsync(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}