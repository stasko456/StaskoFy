using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Playlist;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace StaskoFy.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playlistService;
        private readonly ISongService songService;

        public PlaylistController(IPlaylistService _playlistService, ISongService _songService)
        {
            this.playlistService = _playlistService;
            this.songService = _songService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> IndexForCurrentLoggedUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var playlists = await playlistService.GetAllFromCurrentLoggedUserAsync(Guid.Parse(userId));
            return View(playlists);
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Create()
        {
            // maybe have to do another method
            var songs = await songService.GetAllAsync();

            var model = new PlaylistCreateViewModel
            {
                Songs = new MultiSelectList(songs, "Id", "Title")
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Create(PlaylistCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.GetAllAsync();

            if (!ModelState.IsValid)
            {
                model.Songs = new MultiSelectList(songs, "Id", "Title");
                return View(model);
            }

            await playlistService.AddAsync(model, Guid.Parse(userId));
            return RedirectToAction("IndexForCurrentLoggedUser");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var playlist = await playlistService.GetByIdAsync(id);

            var songs = await songService.GetAllAsync();

            var model = new PlaylistEditViewModel
            {
                Id = id,
                Title = playlist.Title,
                Hours = playlist.Hours,
                Minutes = playlist.Minutes,
                Seconds = playlist.Seconds,
                SongCount = playlist.SongCount,
                DateCreated = playlist.DateCreated,
                ImageURL = playlist.ImageURL,
                IsPublic = playlist.IsPublic,
                Songs = new MultiSelectList(songs, "Id", "Title")
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Edit(PlaylistEditViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.GetAllAsync();

            if (!ModelState.IsValid)
            {
                model.Songs = new MultiSelectList(songs, "Id", "Title");
                return View(model);
            }

            await playlistService.UpdateAsync(model, Guid.Parse(userId));
            return RedirectToAction("IndexForCurrentLoggedUser");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            await playlistService.RemoveAsync(id);
            return RedirectToAction("IndexForCurrentLoggedUser");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrAdminOrUser")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var playlist = await playlistService.GetByIdWithSongsAsync(id);
            return View(playlist);
        }
    }
}
