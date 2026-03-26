using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Enums;
using StaskoFy.ViewModels.Playlist;
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
        private readonly IRepository<Artist> artistRepo;

        public UserService(UserManager<User> _userManager,
                           IRepository<Artist> _artistRepo)
        {
            this.userManager = _userManager;
            this.artistRepo = _artistRepo;
        }

        public async Task<int> GetTotalPagesAsync(int pageSize = 8)
        {
            var totalSongs = await userManager.Users
                .CountAsync();

            return (int)Math.Ceiling(totalSongs / (double)pageSize);
        }

        public async Task<IEnumerable<UserIndexViewModel>> FilteredUsersWithoutAdminAsync(Guid adminId, Guid currentLoggedUserId, string username, int pageNumber = 1, int pageSize = 8)
        {
            var query = userManager.Users
                .Where(u => u.Id != adminId && u.Id != currentLoggedUserId);

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u =>
                EF.Functions.Like(u.UserName, $"%{username}%"));
            }

            return await query
                .Select(u => new UserIndexViewModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    ProfilePicture = u.ImageURL,
                }).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<UserWithPlaylistsViewModel?> GetUserWithPlaylistsByIdAsync(Guid id)
        {
            var user = await userManager.Users
                .Where(u => u.Id == id)
                .Select(u => new UserWithPlaylistsViewModel
                {
                    Id = id,
                    Username = u.UserName,
                    ProfilePicture = u.ImageURL,
                    Playlists = u.Playlists.Select(p => new PlaylistIndexViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Hours = p.Length.Hours,
                        Minutes = p.Length.Minutes,
                        Seconds = p.Length.Seconds,
                        SongCount = p.PlaylistsSongs.Where(ps => ps.PlaylistId == p.Id).Count(),
                        DateCreated = p.DateCreated,
                        ImageURL = p.ImageURL,
                        IsPublic = p.IsPublic,
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (user is null)
            {
                throw new NullReferenceException("Unable to find this user!");
            }

            return user;
        }

        public async Task<bool> IsUserArtistAsync(Guid userId)
        {
            return await artistRepo.GetAllAttached()
                .AnyAsync(u => u.UserId == userId);
        }
    }
}
