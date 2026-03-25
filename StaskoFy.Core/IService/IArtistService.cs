using StaskoFy.ViewModels.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StaskoFy.Core.IService
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistIndexViewModel>> GetFilteredArtistsAsync(Guid userId, string username);

        Task<ArtistIndexViewModel?> GetArtistByIdAsync(Guid id);

        Task AddArtistAsync(ArtistCreateViewModel model);

        Task RemoveArtistAsync(Guid id);

        Task<IEnumerable<ArtistSelectViewModel>> PopulateArtistSelectListAsync(Guid userId);

        Task<ArtistIndexWithProjects?> GetArtistByIdWithProjectsAsync(Guid userId);
    }
}
