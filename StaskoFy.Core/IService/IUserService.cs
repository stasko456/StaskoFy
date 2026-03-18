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
        Task<UserIndexViewModel> GetUsersWithoutAdminAsync(Guid userId);
    }
}
