  using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Album;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaskoFy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IArtistService artistService;
        private readonly ISongService songService;

        public AlbumController(IAlbumService _albumService, IArtistService _artistService, ISongService _songService)
        {
            this.albumService = _albumService;
            this.artistService = _artistService;
            this.songService = _songService;
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> IndexForLoggedArtist()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var albums = await albumService.GetSpecificArtistAlbumsAsync(Guid.Parse(userId));
            return View(albums);
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> IndexForAllUsers()
        {
            var albums = await albumService.GetAllAsync();
            return View(albums);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<ActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var songs = await songService.GetSpecificArtistSongsAsync(Guid.Parse(userId));

            var model = new AlbumCreateViewModel
            {
                Artists = new MultiSelectList(artists, "Id", "Username"),
                Songs = new MultiSelectList(songs, "Id", "Title"),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Create(AlbumCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");

                var songs = await songService.GetSpecificArtistSongsAsync(Guid.Parse(userId));
                model.Songs = new MultiSelectList(songs, "Id", "Title");

                return View(model);
            }

            await albumService.AddAsync(model, Guid.Parse(userId));
            return RedirectToAction("IndexForLoggedArtist");
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var album = await albumService.GetByIdAsync(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var songs = await songService.GetSpecificArtistSongsAsync(Guid.Parse(userId));

            var model = new AlbumEditViewModel
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                ImageURL = album.ImageURL,
                Artists = new MultiSelectList(artists, "Id", "Username"),
                Songs = new MultiSelectList(songs, "Id", "Title"),
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(AlbumEditViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");
                
                var songs = await songService.GetSpecificArtistSongsAsync(Guid.Parse(userId));
                model.Songs = new MultiSelectList(songs, "Id", "Title");
                
                return View(model);
            }

            await albumService.UpdateAsync(model, Guid.Parse(userId));
            return RedirectToAction("IndexForLoggedArtist");
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await albumService.RemoveAsync(id);
            return RedirectToAction("IndexForLoggedArtist");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var album = await albumService.GetByIdWithSongsAsync(id);
            return View(album);
        }
    }
}