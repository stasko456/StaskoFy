using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaskoFy.Core.IService;
using StaskoFy.Core.Service;
using StaskoFy.DataAccess;
using StaskoFy.DataAccess.Repository;
using StaskoFy.Models.Entities;
using StaskoFy.Models.Utilities;

namespace StaskoFy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<StaskoFyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<StaskoFyDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ArtistOrAdmin", policy =>
                    policy.RequireRole("Admin", "Artist"));

                options.AddPolicy("ArtistOrAdminOrUser", policy =>
                    policy.RequireRole("Artist", "Admin", "User"));
            });


            var cloudinarySettings = builder.Configuration
                .GetSection("CloudinarySettings").Get<CloudinarySettings>();
            builder.Services.AddSingleton<Cloudinary>((sp) =>
            {
                return new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey,
                    cloudinarySettings.ApiSecret));
            });

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IArtistService, ArtistService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<ISongService, SongService>();
            builder.Services.AddScoped<IAlbumService, AlbumService>();
            builder.Services.AddScoped<ILikedSongsService, LikedSongsService>();
            builder.Services.AddScoped<IPlaylistService, PlaylistService>();

            // Users:
            // stasko456; Stasko1234*; stdimov2007@gmail.com User
            // Ken Karson; kencarson@gmail.com; KenCarson1234* Artist

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await DataSeeder.SeedRolesAsync(scope.ServiceProvider);
                await DataSeeder.SeedAdminUser(scope.ServiceProvider);
                await DataSeeder.GiveRolesRemainingUsers(scope.ServiceProvider);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}