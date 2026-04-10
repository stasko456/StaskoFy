using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StaskoFy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    ImageURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
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
                    ImageURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAccepted = table.Column<int>(type: "int", nullable: false)
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
                    DateCreated = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
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
                    ImageURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AudioURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CloudinaryAudioPublicId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "CloudinaryPublicId", "ImageURL", "Length", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_nhsydj", "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg", new TimeSpan(0, 0, 14, 33, 0), new DateOnly(2024, 7, 5), 1, "A Great Chaos (Deluxe)" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_bb4lno", "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 20, 12, 0), new DateOnly(2017, 10, 20), 1, "Super Slimey" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "pray_for_paris_rx4tq8", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 11, 17, 0), new DateOnly(2020, 4, 17), 1, "Pray For Paris" },
                    { new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_sfd1rw", "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 24, 14, 0), new DateOnly(2023, 3, 31), 1, "CALL ME IF YOU GET LOST: The Estate Sale" },
                    { new Guid("51111111-1111-1111-1111-111111111111"), "xtended_wemgwk", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 12, 12, 0), new DateOnly(2022, 10, 31), 1, "XTENDED" },
                    { new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 17, 12, 0), new DateOnly(2025, 9, 22), 1, "ᐸ/3³" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CloudinaryPublicId", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageURL", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), 0, "ken_carson_amg20i", "027b0f8a-514e-4d28-880b-5050c6c9c48d", "kenCarson@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/ken_carson_amg20i.jpg", false, null, "kenCarson@gmail.com", "kenCarson", "AQAAAAIAAYagAAAAEHBW5MIExn+T40FM7eQxSsCokm6MBF+2MrPxeNIwhG7gtlVmvkp6+Ek7eZiDDhC0MQ==", null, false, "01111111-1111-1111-1111-111111111111", false, "kenCarson" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), 0, "future_pbmahw", "fbda6603-b195-42ee-80d8-67160b920b92", "future@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/future_pbmahw.jpg", false, null, "future@gmail.com", "future", "AQAAAAIAAYagAAAAECAuJ7VgtzMoxV7HNHZJmALJRsfFY8NLqwjLi/SEjAw4q8RBK7Qy5K+k0K8Zio35lQ==", null, false, "02111111-1111-1111-1111-111111111111", false, "future" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), 0, "young_thug_wz2fln", "147e0a50-561f-46ba-8d2e-9e9debff35e1", "youngThug@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698511/young_thug_wz2fln.jpg", false, null, "youngThug@gmail.com", "youngThug", "AQAAAAIAAYagAAAAEMJVxNcqpzy91g0Rxet0MzuzP5QK/Sf4a6SigWTS67akqm0J7CwvNWr25OHHu0gNVA==", null, false, "03111111-1111-1111-1111-111111111111", false, "youngThug" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), 0, "westside_gunn_vm7xf2", "f2ab3265-7a75-4a3f-aead-b7f9653d9813", "westsideGunn@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698510/westside_gunn_vm7xf2.jpg", false, null, "westsideGunn@gmail.com", "westsideGunn", "AQAAAAIAAYagAAAAEPd41B0Q1SKa92tzMFBuy+TYm8Cz8mw1sRzpWUtRUqt7h3RdF4xZZZh23w2MMWryXQ==", null, false, "04111111-1111-1111-1111-111111111111", false, "westsideGunn" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), 0, "tyler_the_creator_i9yhnu", "874f8298-df14-4bb7-98b4-b46c3340754e", "tylerTheCreator@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698509/tyler_the_creator_i9yhnu.jpg", false, null, "tylerTheCreator@gmail.com", "tylerTheCreator", "AQAAAAIAAYagAAAAEOJEMmw6/2eVVrEI7aj/dGM4aU4VqEoE0/MUN5AWn1beTRWtgEGXDWhhiV6LYdOROQ==", null, false, "05111111-1111-1111-1111-111111111111", false, "tylerTheCreator" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), 0, "destroy_lonely_hmhymx", "2335a65f-f252-4b53-a0e1-5427121ea629", "destroyLonely@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698502/destroy_lonely_hmhymx.jpg", false, null, "destroyLonely@gmail.com", "destroyLonely", "AQAAAAIAAYagAAAAEJHHcjmy8Kd9oEkIzCaCcPYjkSfwZ2Ot9sZ3eaFbqQJ8M8dPt3jK9UwtITsayl4FNQ==", null, false, "06111111-1111-1111-1111-111111111111", false, "destroyLonely" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), 0, "joey_bada_t4ig6u", "33538d78-2843-49d4-ab51-70c8c125c114", "joeyBada$$@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698505/joey_bada_t4ig6u.jpg", false, null, "joeyBada$$@gmail.com", "joeyBada$$", "AQAAAAIAAYagAAAAEDkrtKf/TvwaVYo2FkARd8w78xra+iDdpN8Fnvb9scLPMPcq6NJwbJBzYw3OqhJofQ==", null, false, "07111111-1111-1111-1111-111111111111", false, "joeyBada$$" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), 0, "billie_essco_tqdmip", "908bd548-28f5-410a-b32d-72017dd6c15f", "billiEssco@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698501/billie_essco_tqdmip.jpg", false, null, "billiEssco@gmail.com", "billiEssco", "AQAAAAIAAYagAAAAEGcIZ0xv+pzTcInnt7VbVFy1OqOF7nGn+XsSH7fTaVlvevt2STJx8wLhoEQOxCOZSg==", null, false, "08111111-1111-1111-1111-111111111111", false, "billiEssco" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), 0, "lil_wayne_pqbiny", "9dfe52ba-b8be-4cc2-9875-a70fb539f5b3", "lilWayne@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/lil_wayne_pqbiny.jpg", false, null, "lilWayne@gmail.com", "lilWayne", "AQAAAAIAAYagAAAAEPL8ePDabeB2zh52LjPNzHI3Y7Ff3ec58O3ZYfZvum8MNCaKRcI7bHBf86W+IoJALw==", null, false, "09111111-1111-1111-1111-111111111111", false, "lilWayne" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), 0, "homixide_gang_anf7iv", "e3c90557-21ec-47b6-8a2a-48379cfdff61", "homixideGang@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698504/homixide_gang_anf7iv.jpg", false, null, "homixideGang@gmail.com", "homixideGang", "AQAAAAIAAYagAAAAEAscPQW01CnlqoVMlmP8X+fh/0xcDvqaH76K3/0Z6hD3Pe7G1vVrkjPlt+boBWxVFg==", null, false, "10111111-1111-1111-1111-111111111111", false, "homixideGang" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "stasko456_hblwlq", "d3477a8c-b69d-4340-b57b-05cbf40ff749", "stdimov2007@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/stasko456_hblwlq.jpg", false, null, "stdimov2007@gmail.com", "stasko456", "AQAAAAIAAYagAAAAEHmp1E95WoLUGppuEvSApSDZok5uAUC6JuaiUW2pcwB6AR/eFW+CVmM6bdvUv+K+mg==", null, false, "11111111-1111-1111-1111-111111111111", false, "stasko456" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), 0, "simon333_fafgdv", "b20c33db-4fb5-47d1-a549-ea1425cadd82", "simon2403e8@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/simon333_fafgdv.jpg", false, null, "simon2403e8@gmail.com", "simon333", "AQAAAAIAAYagAAAAEFqkWeOeJ8sAk9Xf7XDohIZ6YEJdBOohd5G60Qci7J8VCE39lYDtB7vWUlc+NLstQA==", null, false, "12111111-1111-1111-1111-111111111111", false, "simon333" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), 0, "n_peew07_yoj6ay", "ba7bfe79-d0f7-4fcf-818e-0fe8fb8b5b1e", "nikolaPeew@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698507/n_peew07_yoj6ay.jpg", false, null, "nikolaPeew@gmail.com", "n_peew07", "AQAAAAIAAYagAAAAECb8paGrqxa0v/tTFTODmE0HQkwdvNpkFVxB4YPzboH1GTNPXjU83abgWb8RWCoquA==", null, false, "13111111-1111-1111-1111-111111111111", false, "n_peew07" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), 0, "gt_baby_gdk5le", "b394b664-e563-45cd-bfcb-8b989c03b459", "gtonev@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/gt_baby_gdk5le.jpg", false, null, "gtonev@gmail.com", "g_tonev", "AQAAAAIAAYagAAAAEAw5uoprWyKTdtgEI1aVrfaNZ2k4btFsfu5m06J5ZGc+pe34kNsMqVvbcT+zrTjJ/A==", null, false, "14111111-1111-1111-1111-111111111111", false, "g_tonev" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), 0, "adasha_quhjni", "1e7baeae-17f7-4f50-b2ce-ac983edca3a5", "nikolaGragov@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1773127136/adasha_quhjni.png", false, null, "nikolaGragov@gmail.com", "niksy_g", "AQAAAAIAAYagAAAAECHdobfHQ6XholfYbsqKxUWUnTRSlEfCwJv7MfhfOe06bbvQH/u4Xoy7UXopybm6vQ==", null, false, "15111111-1111-1111-1111-111111111111", false, "niksy_g" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-111111111111"), "Rock" },
                    { new Guid("11011111-1111-1111-1111-111111111111"), "Metal" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Hip-Hop" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), "Indie Pop" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), "Country" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), "Soul" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), "Opera" },
                    { new Guid("16111111-1111-1111-1111-111111111111"), "Lo-fi" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "Mumble Rap" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "Trap" },
                    { new Guid("41111111-1111-1111-1111-111111111111"), "Hypertrap" },
                    { new Guid("51111111-1111-1111-1111-111111111111"), "Boom Bap" },
                    { new Guid("61111111-1111-1111-1111-111111111111"), "House" },
                    { new Guid("71111111-1111-1111-1111-111111111111"), "Jazz" },
                    { new Guid("81111111-1111-1111-1111-111111111111"), "Pop" },
                    { new Guid("91111111-1111-1111-1111-111111111111"), "Pop-Folk" }
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "IsAccepted", "UserId" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-111111111111"), 1, new Guid("10111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1, new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), 1, new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), 1, new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("41111111-1111-1111-1111-111111111111"), 1, new Guid("04111111-1111-1111-1111-111111111111") },
                    { new Guid("51111111-1111-1111-1111-111111111111"), 1, new Guid("05111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), 1, new Guid("06111111-1111-1111-1111-111111111111") },
                    { new Guid("71111111-1111-1111-1111-111111111111"), 1, new Guid("07111111-1111-1111-1111-111111111111") },
                    { new Guid("81111111-1111-1111-1111-111111111111"), 1, new Guid("08111111-1111-1111-1111-111111111111") },
                    { new Guid("91111111-1111-1111-1111-111111111111"), 1, new Guid("09111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Playlists",
                columns: new[] { "Id", "CloudinaryPublicId", "DateCreated", "ImageURL", "IsPublic", "Length", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "hip-hop-trap-filmar_xxacji", new DateOnly(2024, 12, 4), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556133/hip-hop-trap-filmar_xxacji.jpg", true, new TimeSpan(0, 0, 16, 59, 0), "Hip-Hop & Trap Filmar", new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "opium_filmar_ntgsib", new DateOnly(2024, 12, 10), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698406/opium_filmar_ntgsib.jpg", true, new TimeSpan(0, 0, 19, 9, 0), "00PIUM Filmar", new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "mnooo_cherno_zrfd5j", new DateOnly(2022, 6, 27), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698405/mnooo_cherno_zrfd5j.jpg", false, new TimeSpan(0, 0, 22, 29, 0), "Mnooo Cherno", new Guid("03111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "AudioURL", "CloudinaryAudioPublicId", "CloudinaryPublicId", "GenreId", "ImageURL", "Length", "Likes", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203109/Ken_Carson_-_Green_Room_Official_Audio_cksiai.mp3", "Ken_Carson_-_Green_Room_Official_Audio_cksiai", "a_great_chaos_deluxe_nhsydj", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg", new TimeSpan(0, 0, 3, 8, 0), 6, new DateOnly(2024, 7, 5), 1, "Green Room" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203106/Ken_Carson_-_Lose_It_Official_Audio_l2wjgb.mp3", "Ken_Carson_-_Lose_It_Official_Audio_l2wjgb", "a_great_chaos_deluxe_nhsydj", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg", new TimeSpan(0, 0, 2, 20, 0), 3, new DateOnly(2024, 7, 5), 1, "Lose It" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_-_Me_N_My_Kup_Official_Audio_hfuxfy.mp3", "Ken_Carson_-_Me_N_My_Kup_Official_Audio_hfuxfy", "a_great_chaos_deluxe_nhsydj", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg", new TimeSpan(0, 0, 3, 54, 0), 2, new DateOnly(2024, 7, 5), 1, "Me N My Kup" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_Destroy_Lonely_-_Paranoid_Official_Audio_axsypt.mp3", "Ken_Carson_Destroy_Lonely_-_Paranoid_Official_Audio_axsypt", "a_great_chaos_deluxe_nhsydj", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg", new TimeSpan(0, 0, 2, 7, 0), 4, new DateOnly(2024, 7, 5), 1, "Paranoid" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_-_ss_Official_Audio_v0ttsl.mp3", "Ken_Carson_-_ss_Official_Audio_v0ttsl", "a_great_chaos_deluxe_nhsydj", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/a_great_chaos_deluxe_nhsydj.jpg", new TimeSpan(0, 0, 3, 4, 0), 0, new DateOnly(2024, 7, 5), 1, "ss" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203094/Future_Young_Thug_-_No_Cap_Official_Audio_vguatf.mp3", "Future_Young_Thug_-_No_Cap_Official_Audio_vguatf", "super_slimey_bb4lno", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 2, 24, 0), 3, new DateOnly(2017, 10, 20), 1, "No Cap" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203100/Future_Young_Thug_-_All_Da_Smoke_Official_Audio_qxk6gu.mp3", "Future_Young_Thug_-_All_Da_Smoke_Official_Audio_qxk6gu", "super_slimey_bb4lno", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 3, 24, 0), 0, new DateOnly(2017, 10, 20), 1, "All da Smoke" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Future_Young_Thug_-_200_Official_Audio_olbfa7.mp3", "Future_Young_Thug_-_200_Official_Audio_olbfa7", "super_slimey_bb4lno", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 2, 26, 0), 5, new DateOnly(2017, 10, 20), 1, "200" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203126/Young_Thug_-_Cruise_Ship_Official_Audio_vbju6d.mp3", "Young_Thug_-_Cruise_Ship_Official_Audio_vbju6d", "super_slimey_bb4lno", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 2, 46, 0), 0, new DateOnly(2017, 10, 20), 1, "Cruise Ship" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Future_-_Feed_Me_Dope_Official_Audio_ewzphn.mp3", "Future_-_Feed_Me_Dope_Official_Audio_ewzphn", "super_slimey_bb4lno", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 2, 46, 0), 0, new DateOnly(2017, 10, 20), 1, "Feed Me Dope" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203129/Young_Thug_-_Killed_Before_Official_Audio_zjwb5d.mp3", "Young_Thug_-_Killed_Before_Official_Audio_zjwb5d", "super_slimey_bb4lno", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556405/super_slimey_bb4lno.jpg", new TimeSpan(0, 0, 3, 40, 0), 0, new DateOnly(2017, 10, 20), 1, "Killed Before" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203123/Westside_Gunn_-_No_Vacancy_Audio_bq6czj.mp3", "Westside_Gunn_-_No_Vacancy_Audio_bq6czj", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 1, 35, 0), 0, new DateOnly(2020, 4, 17), 1, "No Vacancy" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203089/327_feat._Tyler_The_Creator_Billie_Essco_r4lgdt.mp3", "327_feat._Tyler_The_Creator_Billie_Essco_r4lgdt", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 5, 49, 0), 0, new DateOnly(2020, 4, 17), 1, "327" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203089/Euro_Step_rdmpx9.mp3", "Euro_Step_rdmpx9", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 1, 49, 0), 0, new DateOnly(2020, 4, 17), 1, "Euro Step" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203135/Versace_hesjgp.mp3", "Versace_hesjgp", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 2, 4, 0), 0, new DateOnly(2020, 4, 17), 1, "Versace" },
                    { new Guid("16111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203100/HOT_WIND_BLOWS_Audio_yldqrr.mp3", "HOT_WIND_BLOWS_Audio_yldqrr", "call_me_if_you_get_lost_the_estate_sale_sfd1rw", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 2, 35, 0), 0, new DateOnly(2023, 3, 31), 1, "HOT WIND BLOWS" },
                    { new Guid("17111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203130/WILSHIRE_Audio_z3sjfq.mp3", "WILSHIRE_Audio_z3sjfq", "call_me_if_you_get_lost_the_estate_sale_sfd1rw", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 8, 35, 0), 0, new DateOnly(2023, 3, 31), 1, "WILSHIRE" },
                    { new Guid("18111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203119/SAFARI_Audio_ezvcpf.mp3", "SAFARI_Audio_ezvcpf", "call_me_if_you_get_lost_the_estate_sale_sfd1rw", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 2, 57, 0), 0, new DateOnly(2023, 3, 31), 1, "SAFARI" },
                    { new Guid("19111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203127/WHAT_A_DAY_lxaxt0.mp3", "WHAT_A_DAY_lxaxt0", "call_me_if_you_get_lost_the_estate_sale_sfd1rw", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 3, 36, 0), 0, new DateOnly(2023, 3, 31), 1, "WHAT A DAY" },
                    { new Guid("20111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203123/Tyler_The_Creator_-_DOGTOOTH_zrwqrg.mp3", "Tyler_The_Creator_-_DOGTOOTH_zrwqrg", "call_me_if_you_get_lost_the_estate_sale_sfd1rw", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 2, 41, 0), 0, new DateOnly(2023, 3, 31), 1, "DOGTOOTH" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203101/HEAVEN_TO_ME_vfsw3y.mp3", "HEAVEN_TO_ME_vfsw3y", "call_me_if_you_get_lost_the_estate_sale_sfd1rw", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1775556404/call_me_if_you_get_lost_the_estate_sale_sfd1rw.jpg", new TimeSpan(0, 0, 3, 50, 0), 0, new DateOnly(2023, 3, 31), 1, "HEAVEN TO ME" },
                    { new Guid("22111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203086/Bane_loijws.mp3", "Bane_loijws", "bane_v4s8f8", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/bane_v4s8f8.jpg", new TimeSpan(0, 0, 2, 20, 0), 0, new DateOnly(2019, 7, 30), 1, "Bane" },
                    { new Guid("23111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203117/Destroy_Lonely_-_if_looks_could_kill_Official_Audio_rpq9dr.mp3", "Destroy_Lonely_-_if_looks_could_kill_Official_Audio_rpq9dr", "if_looks_could_kill_qesda4", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/if_looks_could_kill_qesda4.jpg", new TimeSpan(0, 0, 3, 14, 0), 5, new DateOnly(2023, 3, 3), 1, "if looks could kill" },
                    { new Guid("24111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/Lil_Wayne_-_Kat_Food_Visualizer_wkbtoy.mp3", "Lil_Wayne_-_Kat_Food_Visualizer_wkbtoy", "kat_food_z2ime5", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698282/kat_food_z2ime5.jpg", new TimeSpan(0, 0, 4, 46, 0), 0, new DateOnly(2023, 9, 1), 1, "Kat Food" },
                    { new Guid("25111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/MDMA_rsk7co.mp3", "MDMA_rsk7co", "xtended_wemgwk", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 48, 0), 3, new DateOnly(2022, 10, 31), 1, "MDMA" },
                    { new Guid("26111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203103/Ken_Carson_-_Freestyle_2_Official_Music_Video_toao3c.mp3", "Ken_Carson_-_Freestyle_2_Official_Music_Video_toao3c", "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 18, 0), 0, new DateOnly(2022, 10, 31), 1, "Freestyle 2" },
                    { new Guid("27111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203087/Delinquent_jhnsw8.mp3", "Delinquent_jhnsw8", "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 45, 0), 0, new DateOnly(2022, 10, 31), 1, "Delinquent" },
                    { new Guid("28111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203102/Ken_Carson_-_Fashion_Habits_Official_Audio_j3mix5.mp3", "Ken_Carson_-_Fashion_Habits_Official_Audio_j3mix5", "xtended_wemgwk", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 21, 0), 0, new DateOnly(2022, 10, 31), 1, "Fashion Habits" },
                    { new Guid("29111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203109/Ken_Carson_-_Lord_Of_Chaos_Official_Music_Video_xzpba7.mp3", "Ken_Carson_-_Lord_Of_Chaos_Official_Music_Video_xzpba7", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 1, 0), 0, new DateOnly(2025, 4, 11), 1, "Lord Of Chaos" },
                    { new Guid("30111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203113/Money_Spread_p2e3go.mp3", "Money_Spread_p2e3go", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 1, 45, 0), 0, new DateOnly(2025, 4, 11), 1, "Money Spread" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775844090/Trap_Jump_bliqoc.mp3", "Trap_Jump_bliqoc", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 30, 0), 0, new DateOnly(2025, 4, 11), 1, "Trap Jump" },
                    { new Guid("32111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775844079/Blakk_Rokkstar_opobvr.mp3", "Blakk_Rokkstar_opobvr", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 50, 0), 0, new DateOnly(2025, 4, 11), 1, "Blakk Rokkstar" },
                    { new Guid("33111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775844082/LiveLeak_h7sqtn.mp3", "LiveLeak_h7sqtn", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 10, 0), 0, new DateOnly(2025, 4, 11), 1, "LiveLeak" },
                    { new Guid("34111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203115/risk_E55GLKSVm-Q_njm5fa.mp3", "risk_E55GLKSVm-Q_njm5fa", "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 3, 0, 0), 4, new DateOnly(2025, 9, 22), 1, "risk" },
                    { new Guid("35111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203086/Destroy_Lonely_-_no_pressure_Official_Audio_pwekj4.mp3", "Destroy_Lonely_-_no_pressure_Official_Audio_pwekj4", "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 2, 18, 0), 0, new DateOnly(2025, 9, 22), 1, "no presure" },
                    { new Guid("36111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203118/stfu_vpl0rx.mp3", "stfu_vpl0rx", "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 3, 21, 0), 0, new DateOnly(2025, 9, 22), 1, "stfu" },
                    { new Guid("37111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203098/jumanji_hdjlcv.mp3", "jumanji_hdjlcv", "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 2, 17, 0), 0, new DateOnly(2025, 9, 22), 1, "jumanji" },
                    { new Guid("38111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/not_the_mayor_cqoh9p.mp3", "not_the_mayor_cqoh9p", "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 2, 25, 0), 0, new DateOnly(2025, 9, 22), 1, "not the mayor" },
                    { new Guid("39111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203119/soooo_high_ysd69j.mp3", "soooo_high_ysd69j", "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 3, 51, 0), 0, new DateOnly(2025, 9, 22), 1, "soooo high" }
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
                    { new Guid("41111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111") }
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
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("29111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("30111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("32111111-1111-1111-1111-111111111111") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("33111111-1111-1111-1111-111111111111") },
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
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("34111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("35111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("36111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("37111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("38111111-1111-1111-1111-111111111111") },
                    { new Guid("61111111-1111-1111-1111-111111111111"), new Guid("39111111-1111-1111-1111-111111111111") },
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
                    { new Guid("2fc2eeb7-d99b-4f7f-8307-97bf4c3551a4"), new DateOnly(2026, 4, 1), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("451068be-51bc-4bfc-b020-71b52a6d2a36"), new DateOnly(2026, 1, 22), new Guid("02111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("48b740c0-21c2-4038-9bf8-f7718fdd03a0"), new DateOnly(2026, 1, 5), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("4b815081-8ba9-4417-a114-6212d1e2fbf5"), new DateOnly(2026, 3, 1), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("6488d9b9-017e-47a0-8197-b8e15c20cf57"), new DateOnly(2026, 2, 10), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("6d302563-eacc-4419-a0fe-6e0c0f642a10"), new DateOnly(2026, 2, 20), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("723a2733-4083-47ee-9f1c-808daf7c0538"), new DateOnly(2026, 3, 10), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("75c53397-f2cd-4bdf-a179-9ae768b9a265"), new DateOnly(2026, 3, 15), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("7ad4036e-85c6-48c4-be27-313fd2e3f885"), new DateOnly(2026, 3, 25), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("8650c7fb-f490-4875-8542-4e8f52cbb0e8"), new DateOnly(2026, 3, 20), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("9d2366ec-10f0-40e9-8865-e2ddf9b1e0de"), new DateOnly(2026, 2, 15), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("a33c8f2c-4067-4a67-b8b0-713b4cb300a6"), new DateOnly(2026, 4, 2), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("a4d44363-364a-4f58-9e18-c39ea0367d1f"), new DateOnly(2026, 4, 12), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("bd25ed56-a970-43de-ad6e-23b4b87d910a"), new DateOnly(2026, 1, 15), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("d65244fb-8acb-4245-907c-d6f621b10599"), new DateOnly(2026, 2, 5), new Guid("03111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("daab0399-6048-46ba-81ee-2b9491d077f7"), new DateOnly(2026, 4, 5), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("e3bc38b5-b42d-4215-ac22-c8ef7542300b"), new DateOnly(2026, 1, 10), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("fdf65ffa-9990-49a3-96e6-461e91e90349"), new DateOnly(2026, 2, 28), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") }
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
