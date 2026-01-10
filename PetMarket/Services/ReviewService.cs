using PetMarket.Models;
using PetMarket.Repositories.Interfaces;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            
            review.DateReview = DateOnly.FromDateTime(DateTime.Now);
            await _reviewRepository.AddAsync(review);
            return await _reviewRepository.SaveChangesAsync() > 0;
        }
    }
}