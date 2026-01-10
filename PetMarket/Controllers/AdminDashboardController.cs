using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin")]
    public class AdminDashboardController : Controller
    {
        [HttpGet("")]
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
