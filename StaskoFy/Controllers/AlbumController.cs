using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.ViewModels.Album;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaskoFy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IArtistService artistService;

        public AlbumController(IAlbumService _albumService, IArtistService _artistService)
        {
            this.albumService = _albumService;
            this.artistService = _artistService;
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
        public async Task<ActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var model = new AlbumCreateViewModel
            {
                Artists = new MultiSelectList(artists, "Id", "Username")
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public async Task<IActionResult> Create(AlbumCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");

                return View(model);
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
                Hours = album.Hours,
                Minutes = album.Minutes,
                Seconds = album.Seconds,
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
