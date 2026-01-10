using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Security.Claims;

namespace PetMarket.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly PetMarketDbContext _context;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, PetMarketDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, false);
        }

        public async Task<IdentityResult> RegisterUserAsync(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return result;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<User?> GetUserByClaimsAsync(ClaimsPrincipal userClaims)
        {
            return await _userManager.GetUserAsync(userClaims);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> IsUserAdminAsync(User user)
        {
            return await _userManager.IsInRoleAsync(user, "Admin");
        }

        public async Task<User> GetUserWithOrdersAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            if (user == null) return null;

            return await _context.Users
                .Include(u => u.Order)
                    .ThenInclude(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
        }
    }
}