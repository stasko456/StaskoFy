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
        Task <IEnumerable<UserIndexViewModel>> GetFilteredUsersWithoutAdminAsync(Guid adminId, Guid currentLoggedUserId, string username);

        Task<UserWithPlaylistsViewModel?> GetUserWithPlaylistsByIdAsync(Guid id);

        Task<bool> IsUserArtistAsync(Guid userId);
    }
}
