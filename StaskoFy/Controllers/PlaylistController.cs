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
using StaskoFy.ViewModels.Pagination;
using DocumentFormat.OpenXml.Wordprocessing;

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
        public async Task<IActionResult> Delete(Guid id, string? returnUrl)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await playlistService.RemovePlaylistAsync(id);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("MyLibraryIndex", "Library");
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> Details(Guid id, string name, int pageNumber = 1)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            int pageSize = 5;
            var playlistSongs = await playlistService.GetPlaylistSongsByIdAsync(id, name, pageNumber, pageSize);
            var playlistInfo = await playlistService.GetPlaylistByIdAsync(id);
            int totalPlaylistSongsPages = await playlistService.GetTotalPlaylistSongsPagesAsync(id, pageSize);

            var viewModel = new PlaylistSongsPaginationViewModel
            {
                PlaylistInfo = playlistInfo,
                Songs = playlistSongs.ToList(),
                TotalPages = totalPlaylistSongsPages,
                CurrentPage = pageNumber,
            };

            if (!playlistSongs.Any())
            {
                ViewData["NoPlaylistSongs"] = " ";
            }

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> AddSongToPlaylist(Guid playlistId, Guid songId, string? returnURL)
        {
            if (songId == Guid.Empty || playlistId == Guid.Empty)
            {
                return BadRequest();
            }

            await playlistService.AddSongToPlaylistAsync(playlistId, songId);

            if (!string.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
            {
                return Redirect(returnURL);
            }
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
