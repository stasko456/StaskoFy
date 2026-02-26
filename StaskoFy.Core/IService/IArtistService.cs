using StaskoFy.ViewModels.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistIndexWithProjects>> GetArtistsAsync(Guid userId);

        Task<ArtistIndexWithProjects?> GetArtistByIdAsync(Guid id);

        Task AddArtistAsync(ArtistViewModel model);

        Task RemoveArtistAsync(Guid id);

        Task<IEnumerable<ArtistSelectViewModel>> PopulateArtistSelectListAsync(Guid userId);

        Task<ArtistIndexWithProjects?> GetArtistByIdWithProjectsAsync(Guid userId);
    }
}
