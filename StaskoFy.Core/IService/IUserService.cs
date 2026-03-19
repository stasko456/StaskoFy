using StaskoFy.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IService
{
    public interface IUserService
    {
        Task<int> GetTotalPagesAsync(int pageSize = 8);

        Task<IEnumerable<UserIndexViewModel>> FilteredUsersWithoutAdminAsync(Guid adminId, Guid currentLoggedUserId, string username, int pageNumber = 1, int pageSize = 8);

        Task<UserWithPlaylistsViewModel?> GetUserWithPlaylistsByIdAsync(Guid id);

        Task<bool> IsUserArtistAsync(Guid userId);
    }
}
