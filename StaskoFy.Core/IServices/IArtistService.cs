using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS = StaskoFy.Core.Models;

namespace StaskoFy.Core.IServices
{
    public interface IArtistService
    {
        IQueryable<MODELS.Artist> GetAll();
        Task<MODELS.Artist> GetByIdAsync(Guid? id);
        Task AddAsync(MODELS.Artist artist);
        Task RemoveAsync(Guid? id);
    }
}