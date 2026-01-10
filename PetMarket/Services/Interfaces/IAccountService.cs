using PetMarket.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PetMarket.Services.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);
        Task<IdentityResult> RegisterUserAsync(User user, string password, string role);
        Task<bool> EmailExistsAsync(string email);
        Task<User?> GetUserByClaimsAsync(ClaimsPrincipal userClaims);
        Task<bool> UpdateUserAsync(User user);
        Task SignOutAsync();
        Task<bool> IsUserAdminAsync(User user);
        Task<User> GetUserWithOrdersAsync(ClaimsPrincipal principal);

    }
}