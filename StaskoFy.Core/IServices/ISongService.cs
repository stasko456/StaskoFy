using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface ISongService
    {
        Task<IEnumerable<SongIndexViewModel>> GetAllAsync();

        Task<IEnumerable<SongIndexViewModel>> GetSpecificArtistSongsAsync(Guid artistId);

        Task<SongIndexViewModel?> GetByIdAsync(Guid id);

        Task AddAsync(SongCreateViewModel model, Guid userId);
        Task AddRangeAsync(IEnumerable<SongCreateViewModel> models);

        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(IEnumerable<Guid> ids);

        Task UpdateAsync(SongEditViewModel model);
    }
}