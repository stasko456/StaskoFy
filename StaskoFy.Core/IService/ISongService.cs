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
        Task<IEnumerable<SongIndexViewModel>> GetAllAsync();

        Task<IEnumerable<SongIndexViewModel>> GetSpecificArtistSongsAsync(Guid artistId);

        Task<SongIndexViewModel?> GetByIdAsync(Guid id);

        Task AddAsync(SongCreateViewModel model, Guid artistId);

        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<Guid> ids);

        Task UpdateAsync(SongEditViewModel model, Guid artistId);

        Task<IEnumerable<SongIndexViewModel>> FilterSongsAsync(string searchItem, List<string> filters);

        Task<IEnumerable<SongIndexViewModel>> FilterSongsForCurrentLoggedArtistAsync(Guid artistId, string searchItem, List<string> filters);
    }
}
