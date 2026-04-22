using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Artist;
using StaskoFy.ViewModels.Pagination;
using StaskoFy.ViewModels.User;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly IArtistService artistService;
        private readonly IUploadService imageService;
        private readonly IUserService userService;
        private readonly IPlaylistService playlistService;
        private readonly ILogger<UserController> logger;

        public UserController(UserManager<User> _userManager,
                              SignInManager<User> _signInManager,
                              RoleManager<IdentityRole<Guid>> _roleManager,
                              IArtistService _artistService,
                              IUploadService _imageService,
                              IUserService _userService,
                              IPlaylistService _playlistService,
                              ILogger<UserController> _logger)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = _roleManager;
            this.artistService = _artistService;
            this.imageService = _imageService;
            this.userService = _userService;
            this.playlistService = _playlistService;
            this.logger = _logger; 
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
        [ValidateAntiForgeryToken]
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
                    IsAccepted = UploadStatus.Pending,
                };

                await artistService.AddArtistAsync(artist);
            }

            if (Request.Headers.ContainsKey("HX-Request"))
            {
                Response.Headers.Add("HX-Redirect", Url.Action("Login", "User"));
                return Ok();
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await userManager.FindByNameAsync(viewModel.Username);

            if (user != null)
            {
                bool isArtist = await userService.IsUserArtistAsync(user.Id);

                if (isArtist == true)
                {
                    var artist = await artistService.FindArtistByUserIdAsync(user.Id);

                    if (artist.IsAccepted == UploadStatus.Approved)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                        if (result.Succeeded)
                        {
                            if (Request.Headers.ContainsKey("HX-Request"))
                            {
                                Response.Headers.Add("HX-Redirect", Url.Action("Index", "Home"));
                                return Ok();
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("ArtistNotAcceptedPage", "Artist");
                    }
                }
                else
                {
                    var result = await signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (Request.Headers.ContainsKey("HX-Request"))
                        {
                            Response.Headers.Add("HX-Redirect", Url.Action("Index", "Home"));
                            return Ok();
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid Login");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
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
        [Authorize(Policy = "ArtistOrUser")]
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
        [Authorize(Policy = "ArtistOrUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(EditProfileViewModel viewModel)
        {
            var user = await userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            user.UserName = viewModel.Username;
            
            if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
            {
                await imageService.DestroyImageAsync(user.CloudinaryPublicId);

                var uploadResult = await imageService.UploadImageAsync(viewModel.ImageFile, viewModel.ImageFile.FileName, "user-profile-pictures");
                user.ImageURL = uploadResult.Url;
                user.CloudinaryPublicId = uploadResult.PublicId;
            }

            await userManager.UpdateAsync(user);
            await signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Details", "User");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Index(string username, int pageNumber = 1)
        {
            int pageSize = 8;
            var admin = await userManager.FindByNameAsync("admin");
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await userService.FilteredUsersWithoutAdminAsync(admin.Id, Guid.Parse(currentUserId), username, pageNumber, pageSize);
            int totalPages = await userService.GetTotalPagesAsync(pageSize);

            var viewModel = new UsersPaginationViewModel
            {
                Users = users.ToList(),
                TotalPages = totalPages,
                CurrentPage = pageNumber,
            };

            if (!users.Any())
            {
                ViewData["NoResult"] = "No users found matching your search.";
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> UserDetails(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                bool isArtist = await userService.IsUserArtistAsync(id);
                if (isArtist == true)
                {
                    return RedirectToAction("ArtistDetails", "Artist", new { artistUserId = id });
                }
                var user = await userService.GetUserWithPlaylistsByIdAsync(id);
                if (!user.Playlists.Any())
                {
                    ViewData["NoPlaylists"] = "This user has not uploaded public playlists!";
                }
                return View(user);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError($"{ex.Message}");
                return RedirectToAction("Error", "Home", new { code = 404 });
            }
        }
    }
}
