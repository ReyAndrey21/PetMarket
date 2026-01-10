using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetMarket.Models;
using PetMarket.Services.Interfaces; // NOU
using System.Threading.Tasks;

namespace PetMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("admin/users")]
    public class UsersController : Controller
    {
        
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListUsers()
        {
            
            var users = await _userService.GetAllUsersSortedAsync();

            return View(users);
        }

    }
}