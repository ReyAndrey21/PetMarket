using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetMarket.Models;
using PetMarket.Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetMarket.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        
        private readonly IAccountService _accountService;
        private readonly IFileService _fileService;

        public AccountController(IAccountService accountService, IFileService fileService)
        {
            _accountService = accountService;
            _fileService = fileService;
        }

        
        [HttpGet("login")]
        public IActionResult Login() => View();

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe = true)
        {
            
            var result = await _accountService.PasswordSignInAsync(email, password, rememberMe);

            if (result.Succeeded)
            {
                var user = await _accountService.GetUserByClaimsAsync(User);

                if (user != null && await _accountService.IsUserAdminAsync(user))
                {
                    return RedirectToAction("Dashboard", "AdminDashboard");
                }

                return RedirectToAction("Profile", "Account");
            }

            ViewBag.Error = "Autentificare eșuată. Verificați emailul și parola.";
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register() => View();

        [HttpPost("register")]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string phoneNumber, string password, string username)
        {
            if (await _accountService.EmailExistsAsync(email))
            {
                ViewBag.Error = "Emailul este deja în uz.";
                return View();
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                UserName = username,
                CreatedAt = DateTime.UtcNow
            };

            
            var result = await _accountService.RegisterUserAsync(user, password, "Client");

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Error = string.Join("; ", result.Errors.Select(e => e.Description));
            return View();
        }


        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
           
            var user = await _accountService.GetUserWithOrdersAsync(User);

            if (user == null)
                return RedirectToAction("Login");

            return View(user);
        }

        [HttpGet("edit-profile")]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _accountService.GetUserByClaimsAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            return View(user);
        }

        [HttpPost("edit-profile")]
        [Authorize]
        public async Task<IActionResult> EditProfile(User updatedUser)
        {
            var user = await _accountService.GetUserByClaimsAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(updatedUser);

            
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.UserName = updatedUser.UserName;

            
            if (updatedUser.ProfilePicture != null && updatedUser.ProfilePicture.Length > 0)
            {
                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    _fileService.DeleteFile(user.ProfilePictureUrl);
                }

                string newUrl = await _fileService.SaveFileAsync(updatedUser.ProfilePicture, "profile_picture");
                user.ProfilePictureUrl = newUrl;
            }

            
            await _accountService.UpdateUserAsync(user);

            TempData["Success"] = "Profilul a fost actualizat cu succes!";
            return RedirectToAction("Profile");
        }

      
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("HomePage", "CustomerDashboard");
        }

        
    }
}