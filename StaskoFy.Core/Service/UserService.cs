using Microsoft.AspNetCore.Identity;
using StaskoFy.Core.IService;
using StaskoFy.Models.Entities;
using StaskoFy.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(UserManager<User> _userManager)
        {
            this.userManager = _userManager;
        }

        public async Task<UserIndexViewModel> GetUsersWithoutAdminAsync(Guid userId)
        {
            return null;
        }
    }
}
