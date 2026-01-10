using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Repositories.Interfaces;

namespace PetMarket.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly PetMarketDbContext _context;
        public ReviewRepository(PetMarketDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.IdProduct == productId)
                .OrderByDescending(r => r.DateReview)
                .ToListAsync();
        }
    }
}