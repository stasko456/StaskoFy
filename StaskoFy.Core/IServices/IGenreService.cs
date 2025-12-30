using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IGenreService
    {
        IQueryable<Genre> GetAll();
        Task<Genre> GetByIdAsync(Guid? id);
        Task AddAsync(Genre genre);
        Task RemoveAsync(Genre genre);
        Task UpdateAsync(Genre genre);
    }
}