using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Playlist;
using StaskoFy.ViewModels.Song;
using System.Security.Claims;

namespace StaskoFy.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly IArtistService artistService;
        private readonly IImageService imageService;
        private readonly IPlaylistService playlistService;

        public SongController(ISongService _songService, IGenreService _genreService, IArtistService _artistService, IImageService _imageService, IPlaylistService _playlistService)
        {
            this.songService = _songService;
            this.genreService = _genreService;
            this.artistService = _artistService;
            this.imageService = _imageService;
            this.playlistService = _playlistService;
        }

        [HttpGet]
        [Authorize(Policy = "ArtistOrUser")]
        public async Task<IActionResult> SongsIndexForAllUsers(string searchItem, List<string> filters)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.FilterSongsAsync(searchItem, filters);

            var playlists = await playlistService.SelectPlaylistsFromCurrentLoggedUserAsync(Guid.Parse(userId));

            var viewModel = new SongsPageViewModel
            {
                Songs = songs.ToList(),
                Playlists = playlists.ToList(),
            };

            if (!songs.Any())
            {
                ViewData["NoResult"] = "No songs found matching your search.";
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await genreService.GetGenresAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
            var model = new SongCreateViewModel
            {
                Artists = new MultiSelectList(artists, "Id", "Username")
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Create(SongCreateViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var genres = await genreService.GetGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");

                return View(model);
            }

            await songService.AddSongAsync(model, Guid.Parse(userId));
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await genreService.GetGenresAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));

            var song = await songService.GetSongByIdAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            var model = new SongEditViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Minutes = song.Minutes,
                Seconds = song.Seconds,
                ReleaseDate = song.ReleaseDate,
                GenreId = song.GenreId,
                Artists = new MultiSelectList(artists, "Id", "Username")
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(SongEditViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                var genres = await genreService.GetGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");

                return View(model);
            }

            await songService.UpdateSongsAsync(model, Guid.Parse(userId));
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var song = await songService.GetSongByIdAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            await songService.RemoveSongAsync(id);
            return RedirectToAction("MyProjectsForCurrentLoggedArtistIndex", "Library");
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> MakeSongSingle(Guid songId, Guid albumId)
        {
            if (songId == Guid.Empty)
            {
                return BadRequest();
            }

            await songService.RemoveSongFromAlbumAsync(songId, albumId);

            return RedirectToAction("Details", "Album", new { id = albumId });
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> AddSongToAlbum(Guid songId, Guid albumId)
        {
            if (songId == Guid.Empty)
            {
                return BadRequest();
            }

            await songService.AddSongToAlbumAsync(songId, albumId);

            return RedirectToAction("Details", "Album", new { id = albumId });
        }
    }
}
