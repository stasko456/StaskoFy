using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IArtistService
    {
        IQueryable<Artist> GetAll();
        Task<Artist> GetByIdAsync(Guid? id);
        Task AddAsync(Artist artist);
        Task RemoveAsync(Artist artist);
        Task UpdateAsync(Artist artist);
    }
}