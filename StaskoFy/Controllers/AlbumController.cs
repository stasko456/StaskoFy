using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Album;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;

        public AlbumController(IAlbumService _albumService)
        {
            this.albumService = _albumService;
        }

        [HttpGet]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> IndexForLoggedArtist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var albums = await albumService.GetSpecificArtistAlbumsAsync(Guid.Parse(userId));
            return View(albums);
        }

        [HttpGet]
        public async Task<IActionResult> IndexForAllUsers()
        {
            var albums = await albumService.GetAllAsync();
            return View(albums);
        }

        [HttpGet]
        [Authorize(Roles = "Artist")]
        public ActionResult Create()
        {
            var model = new AlbumCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Create(AlbumCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.ImageURL == string.Empty || model.ImageURL == null)
            {
                model.ImageURL = "/images/defaults/default-album-cover-art.png";
            }

            await albumService.AddAsync(model, Guid.Parse(userId));
            return RedirectToAction("IndexForLoggedArtist");
        }

        [HttpGet]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var album = await albumService.GetByIdAsync(id);

            var model = new AlbumEditViewModel
            {
                Id = album.Id,
                Title = album.Title,
                Length = album.Length,
                ReleaseDate = album.ReleaseDate,
                ImageURL = album.ImageURL,
            };

            await albumService.UpdateAsync(model);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Edit(AlbumEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await albumService.UpdateAsync(model);
            return RedirectToAction("IndexForLoggedArtist");
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await albumService.RemoveAsync(id);
            return RedirectToAction("IndexForLoggedArtist");
        }
    }
}
