using Microsoft.AspNetCore.Identity;
using StaskoFy.Core.DTOs;
using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.IServices
{
    public interface IUserService
    {
        IQueryable<UserDto> GetAllUsers();
    }
}