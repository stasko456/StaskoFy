using CloudinaryDotNet.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Artist;
using StaskoFy.ViewModels.User;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IArtistService artistService;
        private readonly IImageService imageService;

        public UserController(UserManager<User> _userManager,
                              SignInManager<User> _signInManager,
                              RoleManager<IdentityRole<Guid>> _roleManager,
                              IArtistService _artistService,
                              IImageService _imageService)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = _roleManager;
            this.artistService = _artistService;
            this.imageService = _imageService;
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
            var viewModel = new RegisterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            List<string> roles = new List<string>() { "Artist", "User" };

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(roles);
                return View(viewModel);
            }

            var user = new User
            {
                UserName = viewModel.Username,
                Email = viewModel.EmailAddress,
                ImageURL = "/images/defaults/default-user-pfp.png",
                CloudinaryPublicId = ""
            };

            var result = await userManager.CreateAsync(user, viewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                ViewBag.Roles = new SelectList(roles);
                return View(viewModel);
            }

            await userManager.AddToRoleAsync(user, viewModel.Role);

            if (viewModel.Role == "Artist")
            {
                var artist = new ArtistCreateViewModel
                {
                    UserId = user.Id,
                };

                await artistService.AddArtistAsync(artist);
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
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await userManager.FindByNameAsync(viewModel.Username);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid Login");
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var user = await userManager.GetUserAsync(User);

            var viewModel = new ProfileIndexViewModel
            {
                Username = user.UserName,
                ProfilePicture = user.ImageURL,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var user = await userManager.GetUserAsync(User);

            var viewModel = new EditProfileViewModel
            {
                Username = user.UserName,
                CurrentProfilePicture = user.ImageURL,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(EditProfileViewModel viewModel)
        {
            var user = await userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                return View(viewModel);
            }

            user.UserName = viewModel.Username;
            
            if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
            {
                var uploadResult = await imageService.UploadImageAsync(viewModel.ImageFile, viewModel.ImageFile.FileName, "user-profile-pictures");
                user.ImageURL = uploadResult.Url;
                user.CloudinaryPublicId = uploadResult.PublicId;
            }

            await userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }
    }
}
