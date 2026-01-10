using Microsoft.AspNetCore.Identity;
using PetMarket.Models;
using PetMarket.Services.Interfaces;

namespace PetMarket.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;

        public UsersService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<IEnumerable<User>> GetAllUsersSortedAsync()
        {
            
            var users = _userManager.Users
                .OrderBy(u => u.CreatedAt)
                .ToList();

            return Task.FromResult<IEnumerable<User>>(users);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

       
    }
}