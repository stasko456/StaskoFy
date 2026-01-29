using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Artist;
using StaskoFy.ViewModels.User;

namespace StaskoFy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IArtistService artistService;

        public UserController(UserManager<User> _userManager,
                              SignInManager<User> _signInManager,
                              RoleManager<IdentityRole<Guid>> _roleManager,
                              IArtistService _artistService)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = _roleManager;
            this.artistService = _artistService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Home", "Index");
            }
            List<string> roles = new List<string>() { "Artist", "User" };
            ViewBag.Roles = new SelectList(roles);
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            List<string> roles = new List<string>() { "Artist", "User" };

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(roles);
                return View(model);
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.EmailAddress,
                ImageURL = "/images/defaults/default-user-pfp.png",
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                ViewBag.Roles = new SelectList(roles);
                return View(model);
            }

            await userManager.AddToRoleAsync(user, model.Role);

            if (model.Role == "Artist")
            {
                var artist = new ArtistViewModel
                {
                    UserId = user.Id,
                };

                await artistService.AddAsync(artist);
            }

            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
    }
}
