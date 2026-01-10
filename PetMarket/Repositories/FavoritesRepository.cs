using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;

namespace PetMarket.Repositories
{
    public class FavoritesRepository : Repository<Favorites>, IFavoritesRepository
    {
        private readonly PetMarketDbContext _context;
        public FavoritesRepository(PetMarketDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorites>> GetByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Product)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Favorites?> GetByUserAndProductAsync(string userId, int productId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.IdProduct == productId);
        }
    }
}