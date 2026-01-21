using Microsoft.EntityFrameworkCore.Infrastructure;
using StaskoFy.Core.DTOs;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODELS = StaskoFy.Core.DTOs;

namespace StaskoFy.Core.IServices
{
    public interface IGenreService
    {
        Task<IEnumerable<MODELS.Genre>> GetAllAttached();
        Task<MODELS.Genre> GetByIdAsync(Guid? id);
        Task AddAsync(MODELS.Genre model);
        Task RemoveAsync(Guid? id);
        Task UpdateAsync(MODELS.Genre model);
    }
}