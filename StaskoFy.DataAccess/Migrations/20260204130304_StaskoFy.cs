using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StaskoFy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StaskoFy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Length = table.Column<TimeSpan>(type: "time", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SongsCount = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Length = table.Column<TimeSpan>(type: "time", nullable: false),
                    SongCount = table.Column<int>(type: "int", nullable: false),
                    DataCreated = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Length = table.Column<TimeSpan>(type: "time", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistsAlbums",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistsAlbums", x => new { x.ArtistId, x.AlbumId });
                    table.ForeignKey(
                        name: "FK_ArtistsAlbums_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistsAlbums_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArtistsSongs",
                columns: table => new
                {
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistsSongs", x => new { x.ArtistId, x.SongId });
                    table.ForeignKey(
                        name: "FK_ArtistsSongs_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArtistsSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedSongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedSongs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistsSongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateAdded = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistsSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaylistsSongs_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistsSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "ImageURL", "Length", "ReleaseDate", "SongsCount", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-album-cover-art.png", new TimeSpan(0, 1, 5, 0, 0), new DateOnly(2024, 7, 5), 5, "A Great Chaos (Deluxe)" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "/images/defaults/default-album-cover-art.png", new TimeSpan(0, 0, 40, 49, 0), new DateOnly(2017, 10, 20), 6, "Super Slimey" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "/images/defaults/default-album-cover-art.png", new TimeSpan(0, 0, 36, 25, 0), new DateOnly(2020, 4, 17), 4, "Pray For Paris" },
                    { new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-album-cover-art.png", new TimeSpan(0, 1, 17, 0, 0), new DateOnly(2023, 3, 31), 6, "CALL ME IF YOU GET LOST: The Estate Sale" },
                    { new Guid("51111111-1111-1111-1111-111111111111"), "/images/defaults/default-album-cover-art.png", new TimeSpan(0, 1, 1, 0, 0), new DateOnly(2022, 10, 31), 4, "XTENDED" },
                    { new Guid("61111111-1111-1111-1111-111111111111"), "/images/defaults/default-album-cover-art.png", new TimeSpan(0, 0, 54, 32, 0), new DateOnly(2025, 9, 22), 6, "ᐸ/3³" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageURL", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), 0, "202dade7-7afc-40b7-96f3-89eb6f09a512", "kenCarson@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "kenCarson@gmail.com", "kenCarson", "AQAAAAIAAYagAAAAEDwMULyYGetsYhuNkQt5FFLxt/zhnhifALknY0UKnrsBTf3tkNNIHsJ3/39Xl5Bx4g==", null, false, "01111111-1111-1111-1111-111111111111", false, "kenCarson" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), 0, "8c68b681-fb07-44fd-a88f-fca24a1da50c", "future@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "future@gmail.com", "future", "AQAAAAIAAYagAAAAEBpIw2afzZanIn3zUPEzujozPAIV1ca5YmS4bBcKXuh9oUV8pPXEjjk/n5T3azYD5w==", null, false, "02111111-1111-1111-1111-111111111111", false, "future" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), 0, "34b5d07c-c5d2-49db-8fcd-c4300ee7a229", "youngThug@gmail.com", false, "/images/defaults/default-user-pfp.pnguser-pfp", false, null, "youngThug@gmail.com", "youngThug", "AQAAAAIAAYagAAAAEAQ/xvmGW0NhQSxh1E5AgI9jA1Qxt7wvwqZjHPv6CQk7aAQ6OHdzZyj6+7rjfNvTyA==", null, false, "03111111-1111-1111-1111-111111111111", false, "youngThug" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), 0, "0903bf81-ebc6-4e3e-866d-4134ce37795c", "westsideGunn@gmail.com", false, "/images/defaults/default-user-pfp .png", false, null, "westsideGunn@gmail.com", "westsideGunn", "AQAAAAIAAYagAAAAEKLB4eryetyst11vK1K3xtFsNPm7Qakkq3dYjirlIpuSuH5OcmBwcKAsHVALWsJx9g==", null, false, "04111111-1111-1111-1111-111111111111", false, "westsideGunn" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), 0, "5f2a77bf-c4da-46a4-9a67-6ceaf9eba807", "tylerTheCreator@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "tylerTheCreator@gmail.com", "tylerTheCreator", "AQAAAAIAAYagAAAAEH6hc0s559qB3PyplBtKrjUUxD+LMCfViZh19xYVnxGe5ILajuRKpZPlmIRIrrBKaw==", null, false, "05111111-1111-1111-1111-111111111111", false, "tylerTheCreator" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), 0, "85e6b72f-e5df-413e-b94f-e3d37c79d37f", "destroyLonely@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "destroyLonely@gmail.com", "destroyLonely", "AQAAAAIAAYagAAAAEPyBmJzLCrWDzPHILClNCwC5beUvO6wF57hlnegezUIwtDgFj+U7qSWt0AmY1R6+mg==", null, false, "06111111-1111-1111-1111-111111111111", false, "destroyLonely" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), 0, "76ba52c3-1366-4910-b7d1-e19ca124c4fb", "joeyBada$$@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "joeyBada$$@gmail.com", "joeyBada$$", "AQAAAAIAAYagAAAAEPvM8MSWfdmxr+hcJvciVgH9+2TkXrDqzHRKsoJsqvDK69QG7ZNWifT3wNzsopwXBw==", null, false, "07111111-1111-1111-1111-111111111111", false, "joeyBada$$" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), 0, "e45a1edf-c155-4d02-90e6-c433a357a384", "billiEssco@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "billiEssco@gmail.com", "billiEssco", "AQAAAAIAAYagAAAAEKOrH6OirXcuas4PKz4MiuSGYWSQR+/SV80PQtl7x+Br3nfSuHskWHsuqj8d4qvzhg==", null, false, "08111111-1111-1111-1111-111111111111", false, "billiEssco" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), 0, "c1b7e17c-41fc-4a97-97bb-e83c72334d14", "lilWayne@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "lilWayne@gmail.com", "lilWayne", "AQAAAAIAAYagAAAAEEiSEKQhkVZpA7iFUGRR/Sk+i7mw/KmQoiSIDygVFpSKhIMVUdb0Rn59FhNNCJuPXw==", null, false, "09111111-1111-1111-1111-111111111111", false, "lilWayne" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), 0, "1c9bc3df-b1b1-46de-8fe7-67700384947d", "homixideGang@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "homixideGang@gmail.com", "homixideGang", "AQAAAAIAAYagAAAAEPiUfwDscUV1q2XuY88fDi6jSkdbGK7tVqwvAFxFarRJQ2tufp3USZtQj4Eld9XXUQ==", null, false, "10111111-1111-1111-1111-111111111111", false, "homixideGang" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "63106531-cc70-427a-9822-f057dde48249", "stdimov2007@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "stdimov2007@gmail.com", "stasko456", "AQAAAAIAAYagAAAAEPqQb8+KT2mOwrEDypdCTzWoNYUGuFGPO0z2ucjZFfNnLUnEzkWqcCCPkxqiJAddrg==", null, false, "11111111-1111-1111-1111-111111111111", false, "stasko456" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), 0, "94b57cd3-ce76-46b9-b9a5-b1209be70f89", "simon2403e8@gmail.com", false, "images/defaults/default-user-pfp.png", false, null, "simon2403e8@gmail.com", "simon333", "AQAAAAIAAYagAAAAEDuQNCXqsf1bBht0Fwd7ivC+D28x5ZVfXYIM6BxhrhCJlxESTOn1OuPFe3sDQ5hLvg==", null, false, "12111111-1111-1111-1111-111111111111", false, "simon333" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), 0, "ad5ed334-bd06-4053-a915-ebf267057e7b", "nikolaPeew@gmail.com", false, "/images/defaults/default-user-pfp.png", false, null, "nikolaPeew@gmail.com", "n_peew07", "AQAAAAIAAYagAAAAELZCrbNJwLwfNYFG91u7fjqVQxWprBEL87yxqSuva0Dr6v+cn7NLjodkDXpYTQP/dQ==", null, false, "13111111-1111-1111-1111-111111111111", false, "n_peew07" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Hip-Hop" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "Mumble Rap" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "Trap" },
                    { new Guid("41111111-1111-1111-1111-111111111111"), "Hypertrap" },
                    { new Guid("51111111-1111-1111-1111-111111111111"), "Boom Bap" }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("10111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("04111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("05111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("06111111-1111-1111-1111-111111111111") },
                    { new Guid("71111111-1111-1111-1111-111111111111"), new Guid("07111111-1111-1111-1111-111111111111") },
                    { new Guid("81111111-1111-1111-1111-111111111111"), new Guid("08111111-1111-1111-1111-111111111111") },
                    { new Guid("91111111-1111-1111-1111-111111111111"), new Guid("09111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "DataCreated", "ImageURL", "IsPublic", "Length", "SongCount", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateOnly(2024, 12, 4), "/images/defaults/default-album-cover-art.png", true, new TimeSpan(0, 0, 16, 59, 0), 6, "Hip-Hop & Trap Filmar", new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new DateOnly(2024, 12, 10), "/images/defaults/default-album-cover-art.png", true, new TimeSpan(0, 0, 19, 9, 0), 7, "00PIUMxx Filmar", new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new DateOnly(2022, 6, 27), "/images/defaults/default-album-cover-art.png", false, new TimeSpan(0, 0, 22, 29, 0), 8, "Mnooo Cherno", new Guid("03111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "GenreId", "ImageURL", "Length", "Likes", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 8, 0), 10044245, new DateOnly(2024, 7, 5), "Green Room" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 20, 0), 643545, new DateOnly(2024, 7, 5), "Lose It" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 54, 0), 234566, new DateOnly(2024, 7, 5), "Me N My Kup" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 7, 0), 8384754, new DateOnly(2024, 7, 5), "Paranoid" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 4, 0), 3647835, new DateOnly(2024, 7, 5), "ss" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 24, 0), 125467, new DateOnly(2017, 10, 20), "No Cap" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 24, 0), 125467, new DateOnly(2017, 10, 20), "All da Smoke" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 26, 0), 125467, new DateOnly(2017, 10, 20), "200" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 46, 0), 125467, new DateOnly(2017, 10, 20), "Cruise Ship" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 46, 0), 235678, new DateOnly(2017, 10, 20), "Feed Me Dope" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 40, 0), 98765, new DateOnly(2017, 10, 20), "Killed Before" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 1, 35, 0), 123456, new DateOnly(2020, 4, 17), "No Vacancy" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 5, 49, 0), 654321, new DateOnly(2020, 4, 17), "327" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 1, 49, 0), 234567, new DateOnly(2020, 4, 17), "Euro Step" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 4, 0), 345678, new DateOnly(2020, 4, 17), "Versace" },
                    { new Guid("16111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 35, 0), 456789, new DateOnly(2023, 3, 31), "HOT WIND BLOWS" },
                    { new Guid("17111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 8, 35, 0), 567890, new DateOnly(2023, 3, 31), "WILSHIRE" },
                    { new Guid("18111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 57, 0), 678901, new DateOnly(2023, 3, 31), "SAFARI" },
                    { new Guid("19111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 36, 0), 789012, new DateOnly(2023, 3, 31), "WHAT A DAY" },
                    { new Guid("20111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 41, 0), 890123, new DateOnly(2023, 3, 31), "DOGTOOTH" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 50, 0), 901234, new DateOnly(2023, 3, 31), "HEAVEN TO ME" },
                    { new Guid("22111111-1111-1111-1111-111111111111"), null, new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 20, 0), 12345, new DateOnly(2019, 7, 30), "Bane" },
                    { new Guid("23111111-1111-1111-1111-111111111111"), null, new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 14, 0), 23456, new DateOnly(2023, 3, 3), "if looks could kill" },
                    { new Guid("24111111-1111-1111-1111-111111111111"), null, new Guid("31111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 4, 46, 0), 34567, new DateOnly(2023, 9, 1), "Kat Food" },
                    { new Guid("25111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 48, 0), 45678, new DateOnly(2022, 10, 31), "MDMA" },
                    { new Guid("26111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 18, 0), 78901, new DateOnly(2022, 10, 31), "Freestyle 2" },
                    { new Guid("27111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 45, 0), 56789, new DateOnly(2022, 10, 31), "Delinquent" },
                    { new Guid("28111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 21, 0), 67890, new DateOnly(2022, 10, 31), "Fashion Habits" }
                });

            migrationBuilder.InsertData(
                table: "ArtistsAlbums",
                columns: new[] { "AlbumId", "ArtistId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "ArtistsSongs",
                columns: new[] { "ArtistId", "SongId" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("27111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("04111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("05111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("25111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("26111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("27111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("28111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("06111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("07111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("08111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("10111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("06111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("07111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("08111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("09111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("12111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("13111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("14111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("15111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("13111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("16111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("17111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("18111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("19111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("20111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("04111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("22111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("23111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("25111111-1111-1111-1111-111111111111") },
                    { new Guid("71111111-1111-1111-1111-111111111111"), new Guid("13111111-1111-1111-1111-111111111111") },
                    { new Guid("81111111-1111-1111-1111-111111111111"), new Guid("13111111-1111-1111-1111-111111111111") },
                    { new Guid("91111111-1111-1111-1111-111111111111"), new Guid("16111111-1111-1111-1111-111111111111") },
                    { new Guid("91111111-1111-1111-1111-111111111111"), new Guid("24111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "LikedSongs",
                columns: new[] { "Id", "DateAdded", "SongId", "UserId" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new DateOnly(2024, 7, 6), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new DateOnly(2024, 7, 8), new Guid("02111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new DateOnly(2024, 7, 8), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new DateOnly(2024, 9, 16), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new DateOnly(2025, 1, 30), new Guid("10111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new DateOnly(2023, 12, 5), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new DateOnly(2024, 4, 16), new Guid("05111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new DateOnly(2024, 7, 8), new Guid("20111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new DateOnly(2024, 3, 8), new Guid("19111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new DateOnly(2024, 9, 16), new Guid("27111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateOnly(2024, 9, 16), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("12111111-1111-1111-1111-111111111111"), new DateOnly(2024, 11, 26), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new DateOnly(2023, 1, 29), new Guid("14111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("14111111-1111-1111-1111-111111111111"), new DateOnly(2025, 12, 29), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("15111111-1111-1111-1111-111111111111"), new DateOnly(2025, 12, 22), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("16111111-1111-1111-1111-111111111111"), new DateOnly(2023, 10, 29), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "PlaylistsSongs",
                columns: new[] { "Id", "DateAdded", "PlaylistId", "SongId" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new DateOnly(2007, 12, 3), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("06111111-1111-1111-1111-111111111111") },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new DateOnly(2007, 11, 3), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("07111111-1111-1111-1111-111111111111") },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new DateOnly(2007, 10, 3), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("10111111-1111-1111-1111-111111111111") },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new DateOnly(2007, 9, 3), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new DateOnly(2007, 8, 3), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("20111111-1111-1111-1111-111111111111") },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new DateOnly(2007, 7, 3), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("15111111-1111-1111-1111-111111111111") },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new DateOnly(2007, 6, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new DateOnly(2007, 5, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("04111111-1111-1111-1111-111111111111") },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new DateOnly(2007, 4, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("05111111-1111-1111-1111-111111111111") },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new DateOnly(2007, 3, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("23111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateOnly(2007, 2, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("26111111-1111-1111-1111-111111111111") },
                    { new Guid("12111111-1111-1111-1111-111111111111"), new DateOnly(2007, 1, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("27111111-1111-1111-1111-111111111111") },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new DateOnly(2006, 12, 3), new Guid("21111111-1111-1111-1111-111111111111"), new Guid("28111111-1111-1111-1111-111111111111") },
                    { new Guid("14111111-1111-1111-1111-111111111111"), new DateOnly(2006, 11, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("15111111-1111-1111-1111-111111111111") },
                    { new Guid("15111111-1111-1111-1111-111111111111"), new DateOnly(2006, 10, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("20111111-1111-1111-1111-111111111111") },
                    { new Guid("16111111-1111-1111-1111-111111111111"), new DateOnly(2006, 9, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("16111111-1111-1111-1111-111111111111") },
                    { new Guid("17111111-1111-1111-1111-111111111111"), new DateOnly(2006, 8, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("18111111-1111-1111-1111-111111111111"), new DateOnly(2006, 7, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("19111111-1111-1111-1111-111111111111"), new DateOnly(2006, 6, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("14111111-1111-1111-1111-111111111111") },
                    { new Guid("20111111-1111-1111-1111-111111111111"), new DateOnly(2006, 5, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("22111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new DateOnly(2006, 4, 3), new Guid("31111111-1111-1111-1111-111111111111"), new Guid("24111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsAlbums_AlbumId",
                table: "ArtistsAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistsSongs_SongId",
                table: "ArtistsSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LikedSongs_SongId",
                table: "LikedSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedSongs_UserId",
                table: "LikedSongs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsSongs_PlaylistId",
                table: "PlaylistsSongs",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsSongs_SongId",
                table: "PlaylistsSongs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_AlbumId",
                table: "Songs",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreId",
                table: "Songs",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistsAlbums");

            migrationBuilder.DropTable(
                name: "ArtistsSongs");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LikedSongs");

            migrationBuilder.DropTable(
                name: "PlaylistsSongs");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
