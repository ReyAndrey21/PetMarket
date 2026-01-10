using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetMarket.Models;
using PetMarket.Services.Interfaces;

namespace PetMarket.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesService _favoritesService;
        private readonly UserManager<User> _userManager;

        public FavoritesController(IFavoritesService favoritesService, UserManager<User> userManager)
        {
            _favoritesService = favoritesService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var favorites = await _favoritesService.GetUserFavoritesAsync(userId!);
            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            bool added = await _favoritesService.ToggleFavoriteAsync(userId, productId);
            return Json(new { success = true, added = added });
        }
    }
}