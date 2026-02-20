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
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> PlaylistsIndexForCurrentLoggedUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var playlists = await playlistService.GetPlaylistsFromCurrentLoggedUserAsync(Guid.Parse(userId));
            return View(playlists);
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Create()
        {
            var songs = await songService.SelectSongsAsync();

            var model = new PlaylistCreateViewModel
            {
                Songs = new MultiSelectList(songs, "Id", "Title")
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Create(PlaylistCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.SelectSongsAsync();

            if (!ModelState.IsValid)
            {
                model.Songs = new MultiSelectList(songs, "Id", "Title");
                return View(model);
            }

            await playlistService.AddPlaylistAsync(model, Guid.Parse(userId));
            return RedirectToAction("PlaylistsIndexForCurrentLoggedUser");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var playlist = await playlistService.GetPlaylistByIdAsync(id);

            var songs = await songService.SelectSongsAsync();

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
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Edit(PlaylistEditViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.SelectSongsAsync();

            if (!ModelState.IsValid)
            {
                model.Songs = new MultiSelectList(songs, "Id", "Title");
                return View(model);
            }

            await playlistService.UpdatePlaylistAsync(model, Guid.Parse(userId));
            return RedirectToAction("PlaylistsIndexForCurrentLoggedUser");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await playlistService.RemovePlaylistAsync(id);
            return RedirectToAction("PlaylistsIndexForCurrentLoggedUser");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var playlist = await playlistService.GetPlaylistByIdWithSongsAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }
    }
}
