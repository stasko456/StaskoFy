using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MODELS = StaskoFy.Core.Models; 

namespace StaskoFy.Core.IServices
{
    public interface ISongService
    {
        Task<IEnumerable<MODELS.Song>> GetAllAttached();
        Task<MODELS.Song> GetByIdAsync(Guid? id);
        Task AddAsync(MODELS.Song model);
        Task AddRangeAsync(IEnumerable<MODELS.Song> models);
        Task RemoveAsync(Guid? id);
        Task RemoveRangeAsync(IEnumerable<Guid?> ids);
        Task UpdateAsync(MODELS.Song model);
    }
}