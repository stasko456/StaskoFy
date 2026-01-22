using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistViewModel>> GetAllAsync();

        Task<ArtistViewModel?> GetByIdAsync(Guid id);

        Task AddAsync(ArtistViewModel model);

        Task RemoveAsync(Guid id);
    }
}