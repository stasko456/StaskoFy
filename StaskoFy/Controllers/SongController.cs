using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StaskoFy.Core.IService;
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

        public SongController(ISongService _songService, IGenreService _genreService, IArtistService _artistService, IImageService _imageService)
        {
            this.songService = _songService;
            this.genreService = _genreService;
            this.artistService = _artistService;
            this.imageService = _imageService;
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Index(string searchItem, List<string> filters)
        {
            var artistId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var songs = await songService.FilterSongsForCurrentLoggedArtistAsync(Guid.Parse(artistId), searchItem, filters);

            if (!songs.Any())
            {
                ViewData["NoResult"] = "No songs found matching your search.";
            }

            return View(songs);
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await genreService.GetAllAsync();
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
                var genres = await genreService.GetAllAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");

                return View(model);
            }

            var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "songs");

            await songService.AddAsync(model, Guid.Parse(userId), uploadResult.Url, uploadResult.PublicId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await genreService.GetAllAsync();
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));

            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var song = await songService.GetByIdAsync(id);

            var model = new SongEditViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Minutes = song.Minutes,
                Seconds = song.Seconds,
                ReleaseDate = song.ReleaseDate,
                GenreId = song.GenreId,
                ImageURL = song.ImageURL,
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
                var genres = await genreService.GetAllAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                var artists = await artistService.PopulateArtistSelectListAsync(Guid.Parse(userId));
                model.Artists = new MultiSelectList(artists, "Id", "Username");

                return View(model);
            }

            await songService.UpdateAsync(model, Guid.Parse(userId));
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Policy = "Artist")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            var song = await songService.GetByIdAsync(id);

            if (!string.IsNullOrEmpty(song.CloudinaryPublicId))
            {
                await imageService.DestroyImageAsync(song.CloudinaryPublicId);
            }

            await songService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
