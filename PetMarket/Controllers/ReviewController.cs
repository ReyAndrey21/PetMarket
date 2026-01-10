using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Security.Claims;

namespace PetMarket.Controllers
{
    [Authorize] 
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            
            review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                await _reviewService.AddReviewAsync(review);
            }

            
            return RedirectToAction("ViewDetailsProduct", "CustomerDashboard", new { id = review.IdProduct });
        }
    }
}