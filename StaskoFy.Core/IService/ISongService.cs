using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface ISongService
    {
        Task<IEnumerable<SongIndexViewModel>> GetSpecificArtistSongsAsync(Guid artistId);

        Task<SongIndexViewModel?> GetSongByIdAsync(Guid id);

        Task AddSongAsync(SongCreateViewModel model, Guid userId, string imageURL, string publicId);

        Task UpdateSongsAsync(SongEditViewModel model, Guid userId);

        Task RemoveSongAsync(Guid id);
        Task RemoveSongsRangeAsync(IEnumerable<Guid> ids);

        Task<IEnumerable<SongIndexViewModel>> FilterSongsAsync(string searchItem, List<string> filters);

        Task<IEnumerable<SongIndexViewModel>> FilterSongsForCurrentLoggedArtistAsync(Guid userId, string searchItem, List<string> filters);

        Task<IEnumerable<SongSelectViewModel>> SelectSongsAsync();

        Task<IEnumerable<SongSelectViewModel>> SelectSongsByCurrentLoggedArtistAsync(Guid userId);
    }
}
