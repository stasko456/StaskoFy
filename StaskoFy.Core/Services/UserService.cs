using Microsoft.AspNetCore.Identity;
using StaskoFy.Core.DTOs;
using StaskoFy.Core.IServices;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManageer;

        public UserService(UserManager<User> _userManageer)
        {
            this.userManageer = _userManageer;
        }

        public IQueryable<UserDto> GetAllUsers()
        {
            return userManageer.Users.Select(x => new UserDto
            {
                Id = x.Id,
                Username = x.UserName,
                EmailAddress = x.Email
            });
        }
    }
}