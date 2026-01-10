using PetMarket.Models;

namespace PetMarket.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(Review review);
    }
}