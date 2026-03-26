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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_d2vxhf", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 14, 33, 0), new DateOnly(2024, 7, 5), 1, "A Great Chaos (Deluxe)" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 20, 12, 0), new DateOnly(2017, 10, 20), 1, "Super Slimey" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "pray_for_paris_rx4tq8", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 11, 17, 0), new DateOnly(2020, 4, 17), 1, "Pray For Paris" },
                    { new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 24, 14, 0), new DateOnly(2023, 3, 31), 1, "CALL ME IF YOU GET LOST: The Estate Sale" },
                    { new Guid("51111111-1111-1111-1111-111111111111"), "xtended_wemgwk", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 12, 12, 0), new DateOnly(2022, 10, 31), 1, "XTENDED" },
                    { new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 17, 12, 0), new DateOnly(2025, 9, 22), 1, "ᐸ/3³" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CloudinaryPublicId", "ConcurrencyStamp", "Email", "EmailConfirmed", "ImageURL", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), 0, "ken_carson_amg20i", "4ec71677-6442-4d69-a068-d4118ca8a403", "kenCarson@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/ken_carson_amg20i.jpg", false, null, "kenCarson@gmail.com", "kenCarson", "AQAAAAIAAYagAAAAEJIOCteLaIW5vQ8RbW4Z9LaSx/EDbJuGgyPJfUaYqBU6ILbNacV8MF2wqDdqJ61Ndw==", null, false, "01111111-1111-1111-1111-111111111111", false, "kenCarson" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), 0, "future_pbmahw", "ca8c9012-a648-4014-8aac-87f004bc4446", "future@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/future_pbmahw.jpg", false, null, "future@gmail.com", "future", "AQAAAAIAAYagAAAAECvRkCEOZUv0oCNuGt/PHNsWm90cvWAOlL4M2LsJOxxLliHMXw2B6MpY8StAaVZ1NQ==", null, false, "02111111-1111-1111-1111-111111111111", false, "future" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), 0, "young_thug_wz2fln", "988b26e1-943f-43d2-9430-41f1c099c167", "youngThug@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698511/young_thug_wz2fln.jpg", false, null, "youngThug@gmail.com", "youngThug", "AQAAAAIAAYagAAAAENLOXXwadAlDUzkgrge7ailD+b3d0fdr/0WDUMZxZWs17gtBDktyBdp5pQ+KeRMZDg==", null, false, "03111111-1111-1111-1111-111111111111", false, "youngThug" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), 0, "westside_gunn_vm7xf2", "0bde4d63-3ee1-4cc2-a3c9-28508176abaa", "westsideGunn@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698510/westside_gunn_vm7xf2.jpg", false, null, "westsideGunn@gmail.com", "westsideGunn", "AQAAAAIAAYagAAAAEEyKzBGYTAeIZRaGBPNGd/Q12Q2hbrr5x++LJtl2qb2ZUqOa3deW0Tm9h1oDpO7CWw==", null, false, "04111111-1111-1111-1111-111111111111", false, "westsideGunn" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), 0, "tyler_the_creator_i9yhnu", "b0e8f25c-ad6a-44bd-a2b1-79789785fdf1", "tylerTheCreator@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698509/tyler_the_creator_i9yhnu.jpg", false, null, "tylerTheCreator@gmail.com", "tylerTheCreator", "AQAAAAIAAYagAAAAEFDwwhZv8ulUW+40UKLmacqViO9iDHyd7eqi7ZIhxK9RLoalRBD+zcBkY9AMuNXLTw==", null, false, "05111111-1111-1111-1111-111111111111", false, "tylerTheCreator" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), 0, "destroy_lonely_hmhymx", "87050d52-01b4-431b-8972-8320c42ae20b", "destroyLonely@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698502/destroy_lonely_hmhymx.jpg", false, null, "destroyLonely@gmail.com", "destroyLonely", "AQAAAAIAAYagAAAAEMMf/gyBSRcMCzAMK2tS1KkfSxH4K3jpL5rQqqyfVl4onKFITuxWVsdxyUldmdy5xA==", null, false, "06111111-1111-1111-1111-111111111111", false, "destroyLonely" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), 0, "joey_bada_t4ig6u", "b2fcbe6a-ddb9-44fa-8c4a-b5a5ae44a431", "joeyBada$$@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698505/joey_bada_t4ig6u.jpg", false, null, "joeyBada$$@gmail.com", "joeyBada$$", "AQAAAAIAAYagAAAAEBWayLaVOOwzIYr7neaDSStbW/lFExWriZe4K8kkeIBJawEfRn6dZg8R5xj1UIfX2w==", null, false, "07111111-1111-1111-1111-111111111111", false, "joeyBada$$" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), 0, "billie_essco_tqdmip", "5a560d3c-ca8c-4a9a-92b2-4a80fac984ae", "billiEssco@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698501/billie_essco_tqdmip.jpg", false, null, "billiEssco@gmail.com", "billiEssco", "AQAAAAIAAYagAAAAEHVmUp+rKGiKBq8FWz6Ny6eHJJcv+sXOc4vr8px97XekC/WlGDYw2AFajtae8swQKg==", null, false, "08111111-1111-1111-1111-111111111111", false, "billiEssco" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), 0, "lil_wayne_pqbiny", "95f404ca-5341-4611-9ce5-f300324bc789", "lilWayne@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/lil_wayne_pqbiny.jpg", false, null, "lilWayne@gmail.com", "lilWayne", "AQAAAAIAAYagAAAAEELIO5lutOBXMpacnL/LGH5Ub+ng+QOjQ/0/93uuAYNgBU94VVgmGqNN2XdK1uCzyQ==", null, false, "09111111-1111-1111-1111-111111111111", false, "lilWayne" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), 0, "homixide_gang_anf7iv", "31c473e5-0361-4d5b-bbde-06960720af6c", "homixideGang@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698504/homixide_gang_anf7iv.jpg", false, null, "homixideGang@gmail.com", "homixideGang", "AQAAAAIAAYagAAAAEMVvkdMtvCglX3PzhGqJMrWITY+BD3UvgTIBvWPLFUbwlvXkUcsUOyjtNEhHRy8dfQ==", null, false, "10111111-1111-1111-1111-111111111111", false, "homixideGang" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "stasko456_hblwlq", "627716d9-89e8-45d3-b983-002d94d106db", "stdimov2007@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/stasko456_hblwlq.jpg", false, null, "stdimov2007@gmail.com", "stasko456", "AQAAAAIAAYagAAAAEBBBWsT+UcUo16eOjisIv+EKYtSy7NI2JSDxmMcedi4joQ/hsybLRMLU41JBMYCgmw==", null, false, "11111111-1111-1111-1111-111111111111", false, "stasko456" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), 0, "simon333_fafgdv", "256119e9-1023-4f65-bc41-f14e1bb11016", "simon2403e8@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/simon333_fafgdv.jpg", false, null, "simon2403e8@gmail.com", "simon333", "AQAAAAIAAYagAAAAECbQ3nM5QkZAkap9SOioCFK8QYTntVuwdFnQI+u3pegWotKJKayEgIEntbeopUJacA==", null, false, "12111111-1111-1111-1111-111111111111", false, "simon333" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), 0, "n_peew07_yoj6ay", "9d6ef9c0-2030-4574-b37d-6009285a9e36", "nikolaPeew@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698507/n_peew07_yoj6ay.jpg", false, null, "nikolaPeew@gmail.com", "n_peew07", "AQAAAAIAAYagAAAAEBBwSKaZUHz8V7Eheq5M/0EAoNjrSx3U5GkZqL+02RdpKP25ROqghu8ONXshLrb44Q==", null, false, "13111111-1111-1111-1111-111111111111", false, "n_peew07" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), 0, "gt_baby_gdk5le", "5c046b1f-078f-49ca-813b-f38919f43d10", "gtonev@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/gt_baby_gdk5le.jpg", false, null, "gtonev@gmail.com", "g_tonev", "AQAAAAIAAYagAAAAEMWFr8n3LPrtjoPFyxfJqmGu+e3dBNgUAOnv+qwyz21D5qCsSAHa+iY+dO7bfDtGBw==", null, false, "14111111-1111-1111-1111-111111111111", false, "g_tonev" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), 0, "adasha_quhjni", "7f9859c1-a754-4ca2-b72e-9f2acd678a50", "nikolaGragov@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1773127136/adasha_quhjni.png", false, null, "nikolaGragov@gmail.com", "niksy_g", "AQAAAAIAAYagAAAAEOy3LEPvWB32zoMSrZ1P6gSPincZsOMN5uRMQA7bio8z4dza6QejYbG1+t6olHQdIg==", null, false, "15111111-1111-1111-1111-111111111111", false, "niksy_g" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-111111111111"), "Rock", 3 },
                    { new Guid("11011111-1111-1111-1111-111111111111"), "Metal", 3 },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Hip-Hop", 1 },
                    { new Guid("12111111-1111-1111-1111-111111111111"), "Indie Pop", 3 },
                    { new Guid("13111111-1111-1111-1111-111111111111"), "Country", 3 },
                    { new Guid("14111111-1111-1111-1111-111111111111"), "Soul", 1 },
                    { new Guid("15111111-1111-1111-1111-111111111111"), "Opera", 1 },
                    { new Guid("16111111-1111-1111-1111-111111111111"), "Lo-fi", 1 },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "Mumble Rap", 1 },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "Trap", 1 },
                    { new Guid("41111111-1111-1111-1111-111111111111"), "Hypertrap", 1 },
                    { new Guid("51111111-1111-1111-1111-111111111111"), "Boom Bap", 1 },
                    { new Guid("61111111-1111-1111-1111-111111111111"), "House", 1 },
                    { new Guid("71111111-1111-1111-1111-111111111111"), "Jazz", 3 },
                    { new Guid("81111111-1111-1111-1111-111111111111"), "Pop", 3 },
                    { new Guid("91111111-1111-1111-1111-111111111111"), "Pop-Folk", 3 }
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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "hip-hop-trap-filmar_n5y3kx", new DateOnly(2024, 12, 4), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698406/hip-hop-trap-filmar_n5y3kx.jpg", true, new TimeSpan(0, 0, 16, 59, 0), "Hip-Hop & Trap Filmar", new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "opium_filmar_ntgsib", new DateOnly(2024, 12, 10), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698406/opium_filmar_ntgsib.jpg", true, new TimeSpan(0, 0, 19, 9, 0), "00PIUM Filmar", new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "mnooo_cherno_zrfd5j", new DateOnly(2022, 6, 27), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698405/mnooo_cherno_zrfd5j.jpg", false, new TimeSpan(0, 0, 22, 29, 0), "Mnooo Cherno", new Guid("03111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "CloudinaryPublicId", "GenreId", "ImageURL", "Length", "Likes", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_d2vxhf", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 3, 8, 0), 10044245, new DateOnly(2024, 7, 5), 1, "Green Room" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_d2vxhf", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 2, 20, 0), 643545, new DateOnly(2024, 7, 5), 1, "Lose It" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_d2vxhf", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 3, 54, 0), 234566, new DateOnly(2024, 7, 5), 1, "Me N My Kup" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_d2vxhf", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 2, 7, 0), 8384754, new DateOnly(2024, 7, 5), 1, "Paranoid" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_d2vxhf", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 3, 4, 0), 3647835, new DateOnly(2024, 7, 5), 1, "ss" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 24, 0), 125467, new DateOnly(2017, 10, 20), 1, "No Cap" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 3, 24, 0), 125467, new DateOnly(2017, 10, 20), 1, "All da Smoke" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 26, 0), 125467, new DateOnly(2017, 10, 20), 1, "200" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 46, 0), 125467, new DateOnly(2017, 10, 20), 1, "Cruise Ship" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 46, 0), 235678, new DateOnly(2017, 10, 20), 1, "Feed Me Dope" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 3, 40, 0), 98765, new DateOnly(2017, 10, 20), 1, "Killed Before" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 1, 35, 0), 123456, new DateOnly(2020, 4, 17), 1, "No Vacancy" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 5, 49, 0), 654321, new DateOnly(2020, 4, 17), 1, "327" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 1, 49, 0), 234567, new DateOnly(2020, 4, 17), 1, "Euro Step" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 2, 4, 0), 345678, new DateOnly(2020, 4, 17), 1, "Versace" },
                    { new Guid("16111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 2, 35, 0), 456789, new DateOnly(2023, 3, 31), 1, "HOT WIND BLOWS" },
                    { new Guid("17111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 8, 35, 0), 567890, new DateOnly(2023, 3, 31), 1, "WILSHIRE" },
                    { new Guid("18111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 2, 57, 0), 678901, new DateOnly(2023, 3, 31), 1, "SAFARI" },
                    { new Guid("19111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 3, 36, 0), 789012, new DateOnly(2023, 3, 31), 1, "WHAT A DAY" },
                    { new Guid("20111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 2, 41, 0), 890123, new DateOnly(2023, 3, 31), 1, "DOGTOOTH" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 3, 50, 0), 901234, new DateOnly(2023, 3, 31), 1, "HEAVEN TO ME" },
                    { new Guid("22111111-1111-1111-1111-111111111111"), null, "bane_v4s8f8", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/bane_v4s8f8.jpg", new TimeSpan(0, 0, 2, 20, 0), 12345, new DateOnly(2019, 7, 30), 1, "Bane" },
                    { new Guid("23111111-1111-1111-1111-111111111111"), null, "if_looks_could_kill_qesda4", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/if_looks_could_kill_qesda4.jpg", new TimeSpan(0, 0, 3, 14, 0), 23456, new DateOnly(2023, 3, 3), 1, "if looks could kill" },
                    { new Guid("24111111-1111-1111-1111-111111111111"), null, "kat_food_z2ime5", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698282/kat_food_z2ime5.jpg", new TimeSpan(0, 0, 4, 46, 0), 34567, new DateOnly(2023, 9, 1), 1, "Kat Food" },
                    { new Guid("25111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "xtended_wemgwk", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 48, 0), 45678, new DateOnly(2022, 10, 31), 1, "MDMA" },
                    { new Guid("26111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 18, 0), 78901, new DateOnly(2022, 10, 31), 1, "Freestyle 2" },
                    { new Guid("27111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 45, 0), 56789, new DateOnly(2022, 10, 31), 1, "Delinquent" },
                    { new Guid("28111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "xtended_wemgwk", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 21, 0), 67890, new DateOnly(2022, 10, 31), 1, "Fashion Habits" },
                    { new Guid("29111111-1111-1111-1111-111111111111"), null, "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 1, 0), 4566, new DateOnly(2025, 4, 11), 1, "Lord Of Chaos" },
                    { new Guid("30111111-1111-1111-1111-111111111111"), null, "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 1, 45, 0), 55883, new DateOnly(2025, 4, 11), 1, "Money Spread" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), null, "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 30, 0), 4444678, new DateOnly(2025, 4, 11), 1, "Trap Jump" },
                    { new Guid("32111111-1111-1111-1111-111111111111"), null, "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 50, 0), 123876, new DateOnly(2025, 4, 11), 1, "Blakk Rokkstar" },
                    { new Guid("33111111-1111-1111-1111-111111111111"), null, "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 10, 0), 56834, new DateOnly(2025, 4, 11), 1, "LiveLeak" },
                    { new Guid("34111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 3, 0, 0), 478346, new DateOnly(2025, 9, 22), 1, "risk" },
                    { new Guid("35111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 2, 18, 0), 999, new DateOnly(2025, 9, 22), 1, "no presure" },
                    { new Guid("36111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 3, 21, 0), 346651, new DateOnly(2025, 9, 22), 1, "stfu" },
                    { new Guid("37111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 2, 17, 0), 54116, new DateOnly(2025, 9, 22), 1, "jumanji" },
                    { new Guid("38111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 2, 25, 0), 54116, new DateOnly(2025, 9, 22), 1, "not the mayor" },
                    { new Guid("39111111-1111-1111-1111-111111111111"), new Guid("61111111-1111-1111-1111-111111111111"), "broken_hearts_3_xm79ww", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/broken_hearts_3_xm79ww.jpg", new TimeSpan(0, 0, 3, 51, 0), 1222, new DateOnly(2025, 9, 22), 1, "soooo high" }
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
