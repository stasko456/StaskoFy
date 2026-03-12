using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Playlist;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Web.Razor.Generator;

namespace StaskoFy.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playlistService;
        private readonly ISongService songService;

        public PlaylistController(IPlaylistService _playlistService,
                                  ISongService _songService)
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

            var viewModel = new PlaylistCreateViewModel
            {
                Songs = new MultiSelectList(songs, "Id", "Title")
            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Create(PlaylistCreateViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.SelectSongsAsync();

            if (!ModelState.IsValid)
            {
                viewModel.Songs = new MultiSelectList(songs, "Id", "Title");
                return View(viewModel);
            }

            await playlistService.AddPlaylistAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyLibraryIndex", "Library");
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

            if (playlist == null)
            {
                return NotFound();
            }

            var songs = await songService.SelectSongsAsync();

            var viewModel = new PlaylistEditViewModel
            {
                Id = id,
                Title = playlist.Title,
                DateCreated = playlist.DateCreated, 
                IsPublic = playlist.IsPublic,
                Songs = new MultiSelectList(songs, "Id", "Title")
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Edit(PlaylistEditViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.SelectSongsAsync();

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await playlistService.UpdatePlaylistAsync(viewModel, Guid.Parse(userId));
            return RedirectToAction("MyLibraryIndex", "Library");
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
            return RedirectToAction("MyLibraryIndex", "Library");
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

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> AddSongToPlaylist(Guid playlistId, Guid songId)
        {
            if (songId == Guid.Empty || playlistId == Guid.Empty)
            {
                return BadRequest();
            }

            await playlistService.AddSongToPlaylistAsync(playlistId, songId);
            return RedirectToAction("MyLibraryIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> RemoveSongFromPlaylist(Guid playlistId, Guid songId)
        {
            if (songId == Guid.Empty || playlistId == Guid.Empty)
            {
                return BadRequest();
            }

            await playlistService.RemoveSongFromPlaylistAsync(playlistId, songId);
            return RedirectToAction("Details", new { id = playlistId });
        }
    }
}
