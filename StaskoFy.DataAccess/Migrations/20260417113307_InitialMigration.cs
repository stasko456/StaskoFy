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
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
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
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
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
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
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
                    CloudinaryPublicId = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AudioURL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CloudinaryAudioPublicId = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "a_great_chaos_deluxe_jnaj7p", "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078561/a_great_chaos_deluxe_jnaj7p.jpg", new TimeSpan(0, 0, 14, 33, 0), new DateOnly(2024, 7, 5), 1, "A Great Chaos (Deluxe)" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "super_slimey_kxogmj", "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 20, 12, 0), new DateOnly(2017, 10, 20), 1, "Super Slimey" },
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
                    { new Guid("01111111-1111-1111-1111-111111111111"), 0, "ken_carson_amg20i", "2e40adb4-a9ad-4e12-aa41-189c2941c6fb", "kenCarson@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/ken_carson_amg20i.jpg", false, null, "kenCarson@gmail.com", "kenCarson", "AQAAAAIAAYagAAAAEAo+rAGcz1q0Hc87UohwKJrtNllG3VNlA7eXfeA+e4Ojg0ysVEd4knYPw9gLF+QCmg==", null, false, "01111111-1111-1111-1111-111111111111", false, "kenCarson" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), 0, "future_pbmahw", "8b2eac5e-1112-4241-b8a5-9f37e49e081d", "future@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/future_pbmahw.jpg", false, null, "future@gmail.com", "future", "AQAAAAIAAYagAAAAEM28Zv8goeKfUls5oPPGYplbBMzO67zUyLx3E/F71JD/lWubiUWj3vFYrc69xd/ZGA==", null, false, "02111111-1111-1111-1111-111111111111", false, "future" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), 0, "young_thug_wz2fln", "a74feac2-1f43-48eb-8357-6265d9e42972", "youngThug@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698511/young_thug_wz2fln.jpg", false, null, "youngThug@gmail.com", "youngThug", "AQAAAAIAAYagAAAAEBZ4L6VouZmtbsZltRyEqB5ozF9uOGuXhO6I5XveHuH6SSmDg/FxyER96ZShIyFEFA==", null, false, "03111111-1111-1111-1111-111111111111", false, "youngThug" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), 0, "westside_gunn_vm7xf2", "c5f24eb4-ae75-45e2-9f7a-ae4d816d2640", "westsideGunn@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698510/westside_gunn_vm7xf2.jpg", false, null, "westsideGunn@gmail.com", "westsideGunn", "AQAAAAIAAYagAAAAEBbBTZ228vK8dgZ1NDog+ScAsDxu7Y9uZqByMJAIfsZUqLkLUh++9JtZ3JwO+YKe0w==", null, false, "04111111-1111-1111-1111-111111111111", false, "westsideGunn" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), 0, "tyler_the_creator_i9yhnu", "1770e4c0-f813-48a2-a75b-44d97d782b9a", "tylerTheCreator@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698509/tyler_the_creator_i9yhnu.jpg", false, null, "tylerTheCreator@gmail.com", "tylerTheCreator", "AQAAAAIAAYagAAAAEF/8LHh0a5Kn7PAZui/Hh6l9RhqCHkBvkf4vxFlI5KT8FoRD0q9JSkQDwod733IIcw==", null, false, "05111111-1111-1111-1111-111111111111", false, "tylerTheCreator" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), 0, "destroy_lonely_hmhymx", "6575159f-d7fa-4a02-ab8c-e85f0aabeb04", "destroyLonely@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698502/destroy_lonely_hmhymx.jpg", false, null, "destroyLonely@gmail.com", "destroyLonely", "AQAAAAIAAYagAAAAEMFwnvDWhu6TYZ118Nxb11TL6gOJRQ2pKt2k+iEag7K3d54+ULf63H4NR2jzVQ1Hxw==", null, false, "06111111-1111-1111-1111-111111111111", false, "destroyLonely" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), 0, "joey_bada_t4ig6u", "f43d47fd-6f98-42e4-a042-c89e4357f2bf", "joeyBada$$@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698505/joey_bada_t4ig6u.jpg", false, null, "joeyBada$$@gmail.com", "joeyBada$$", "AQAAAAIAAYagAAAAEKfvvKlDQAUepss7hN4hKgoL4+452D3qGR/bQSOSoyuMNyCQU1LZ28Mi9kFI/PN9Vg==", null, false, "07111111-1111-1111-1111-111111111111", false, "joeyBada$$" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), 0, "billie_essco_tqdmip", "e73e0d5d-60cf-4a0b-940e-3a67507f2daf", "billiEssco@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698501/billie_essco_tqdmip.jpg", false, null, "billiEssco@gmail.com", "billiEssco", "AQAAAAIAAYagAAAAEOONBbYYITleUGNwhW2dhD0epxv+taDjD7ittqSpezbwDmsERY4GrJ+mtkHPeR2GrA==", null, false, "08111111-1111-1111-1111-111111111111", false, "billiEssco" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), 0, "lil_wayne_pqbiny", "d0790a11-deab-4fce-bb82-fd9bd44ddeb8", "lilWayne@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/lil_wayne_pqbiny.jpg", false, null, "lilWayne@gmail.com", "lilWayne", "AQAAAAIAAYagAAAAEPk9fItowA/8CBXr+xlstyjIL3XVzWU+sq5cS8CfynbLoL6U47eJOtfWOnERsAWhEg==", null, false, "09111111-1111-1111-1111-111111111111", false, "lilWayne" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), 0, "homixide_gang_anf7iv", "a276a2ab-9e43-49c3-baa2-da0fb8ff36c9", "homixideGang@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698504/homixide_gang_anf7iv.jpg", false, null, "homixideGang@gmail.com", "homixideGang", "AQAAAAIAAYagAAAAEArChWY7/BX2asSYm6tVKqZ9r3nDQUY3wXdbem4U03QesKnUHs+EzHpgTKbuts46Hw==", null, false, "10111111-1111-1111-1111-111111111111", false, "homixideGang" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "stasko456_hblwlq", "11af0089-a403-434f-a60c-48cefbcddc8d", "stdimov2007@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/stasko456_hblwlq.jpg", false, null, "stdimov2007@gmail.com", "stasko456", "AQAAAAIAAYagAAAAEEfoAISgQ/Do+pdSijkVG3ldAX8nrbqsV5ulFuukA32BgbClht6oeJWXss9D/APD8A==", null, false, "11111111-1111-1111-1111-111111111111", false, "stasko456" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), 0, "simon333_fafgdv", "bfed669d-5e89-433c-9ad8-cfa3b0d59c90", "simon2403e8@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/simon333_fafgdv.jpg", false, null, "simon2403e8@gmail.com", "simon333", "AQAAAAIAAYagAAAAEFhGxVZlqCkHZsh7q3StJzrsyHtxWmOXSWsqE29gFC5h/zkmJhRk+Jiy/jA9DtqClw==", null, false, "12111111-1111-1111-1111-111111111111", false, "simon333" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), 0, "n_peew07_yoj6ay", "a6758570-dbe5-4116-816e-4d3a1214b4e8", "nikolaPeew@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698507/n_peew07_yoj6ay.jpg", false, null, "nikolaPeew@gmail.com", "n_peew07", "AQAAAAIAAYagAAAAEO65jogX9RH12srvMa4c2JDBh4TnheG2dXl7mvBY6YPWtvPKKKru5Fom4K4MGY0k7A==", null, false, "13111111-1111-1111-1111-111111111111", false, "n_peew07" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), 0, "gt_baby_gdk5le", "01e8a029-16da-4ec0-81d6-637f6905593c", "gtonev@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/gt_baby_gdk5le.jpg", false, null, "gtonev@gmail.com", "g_tonev", "AQAAAAIAAYagAAAAEEpkDJqbcprVI/4KNew4noElkobSZhL9qkqFkM7viOrMuJVUhrMSM7n+5nJ9OR3a1w==", null, false, "14111111-1111-1111-1111-111111111111", false, "g_tonev" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), 0, "adasha_quhjni", "7dfb1097-dc74-4378-9108-cfc6a6fea58e", "nikolaGragov@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1773127136/adasha_quhjni.png", false, null, "nikolaGragov@gmail.com", "niksy_g", "AQAAAAIAAYagAAAAEHb6qt+nsfRGkWQU7y30XZsAaUv7cy2t9khCtRvS/gxlPQXTvqTyXof++bJVi6iiZQ==", null, false, "15111111-1111-1111-1111-111111111111", false, "niksy_g" }
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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "hip-hop-trap-filmar_wmu6g3", new DateOnly(2024, 12, 4), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078475/hip-hop-trap-filmar_wmu6g3.jpg", true, new TimeSpan(0, 0, 16, 59, 0), "Hip-Hop & Trap Filmar", new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("21111111-1111-1111-1111-111111111111"), "opium_filmar_ntgsib", new DateOnly(2024, 12, 10), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698406/opium_filmar_ntgsib.jpg", true, new TimeSpan(0, 0, 19, 9, 0), "00PIUM Filmar", new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("31111111-1111-1111-1111-111111111111"), "mnooo_cherno_zrfd5j", new DateOnly(2022, 6, 27), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698405/mnooo_cherno_zrfd5j.jpg", false, new TimeSpan(0, 0, 22, 29, 0), "Mnooo Cherno", new Guid("03111111-1111-1111-1111-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "AudioURL", "CloudinaryAudioPublicId", "CloudinaryPublicId", "GenreId", "ImageURL", "Length", "Likes", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203109/Ken_Carson_-_Green_Room_Official_Audio_cksiai.mp3", "Ken_Carson_-_Green_Room_Official_Audio_cksiai", "a_great_chaos_deluxe_jnaj7p", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078561/a_great_chaos_deluxe_jnaj7p.jpg", new TimeSpan(0, 0, 3, 8, 0), 6, new DateOnly(2024, 7, 5), 1, "Green Room" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203106/Ken_Carson_-_Lose_It_Official_Audio_l2wjgb.mp3", "Ken_Carson_-_Lose_It_Official_Audio_l2wjgb", "a_great_chaos_deluxe_jnaj7p", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078561/a_great_chaos_deluxe_jnaj7p.jpg", new TimeSpan(0, 0, 2, 20, 0), 3, new DateOnly(2024, 7, 5), 1, "Lose It" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_-_Me_N_My_Kup_Official_Audio_hfuxfy.mp3", "Ken_Carson_-_Me_N_My_Kup_Official_Audio_hfuxfy", "a_great_chaos_deluxe_jnaj7p", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078561/a_great_chaos_deluxe_jnaj7p.jpg", new TimeSpan(0, 0, 3, 54, 0), 2, new DateOnly(2024, 7, 5), 1, "Me N My Kup" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_Destroy_Lonely_-_Paranoid_Official_Audio_axsypt.mp3", "Ken_Carson_Destroy_Lonely_-_Paranoid_Official_Audio_axsypt", "a_great_chaos_deluxe_jnaj7p", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078561/a_great_chaos_deluxe_jnaj7p.jpg", new TimeSpan(0, 0, 2, 7, 0), 4, new DateOnly(2024, 7, 5), 1, "Paranoid" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_-_ss_Official_Audio_v0ttsl.mp3", "Ken_Carson_-_ss_Official_Audio_v0ttsl", "a_great_chaos_deluxe_jnaj7p", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078561/a_great_chaos_deluxe_jnaj7p.jpg", new TimeSpan(0, 0, 3, 4, 0), 0, new DateOnly(2024, 7, 5), 1, "ss" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203094/Future_Young_Thug_-_No_Cap_Official_Audio_vguatf.mp3", "Future_Young_Thug_-_No_Cap_Official_Audio_vguatf", "super_slimey_kxogmj", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 2, 24, 0), 3, new DateOnly(2017, 10, 20), 1, "No Cap" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203100/Future_Young_Thug_-_All_Da_Smoke_Official_Audio_qxk6gu.mp3", "Future_Young_Thug_-_All_Da_Smoke_Official_Audio_qxk6gu", "super_slimey_kxogmj", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 3, 24, 0), 0, new DateOnly(2017, 10, 20), 1, "All da Smoke" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Future_Young_Thug_-_200_Official_Audio_olbfa7.mp3", "Future_Young_Thug_-_200_Official_Audio_olbfa7", "super_slimey_kxogmj", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 2, 26, 0), 5, new DateOnly(2017, 10, 20), 1, "200" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203126/Young_Thug_-_Cruise_Ship_Official_Audio_vbju6d.mp3", "Young_Thug_-_Cruise_Ship_Official_Audio_vbju6d", "super_slimey_kxogmj", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 2, 46, 0), 0, new DateOnly(2017, 10, 20), 1, "Cruise Ship" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Future_-_Feed_Me_Dope_Official_Audio_ewzphn.mp3", "Future_-_Feed_Me_Dope_Official_Audio_ewzphn", "super_slimey_kxogmj", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 2, 46, 0), 0, new DateOnly(2017, 10, 20), 1, "Feed Me Dope" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203129/Young_Thug_-_Killed_Before_Official_Audio_zjwb5d.mp3", "Young_Thug_-_Killed_Before_Official_Audio_zjwb5d", "super_slimey_kxogmj", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776333860/super_slimey_kxogmj.jpg", new TimeSpan(0, 0, 3, 40, 0), 0, new DateOnly(2017, 10, 20), 1, "Killed Before" },
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
                    { new Guid("22111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203086/Bane_loijws.mp3", "Bane_loijws", "bane_fuzfez", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1776078587/bane_fuzfez.jpg", new TimeSpan(0, 0, 2, 20, 0), 0, new DateOnly(2019, 7, 30), 1, "Bane" },
                    { new Guid("23111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203117/Destroy_Lonely_-_if_looks_could_kill_Official_Audio_rpq9dr.mp3", "Destroy_Lonely_-_if_looks_could_kill_Official_Audio_rpq9dr", "if_looks_could_kill_qesda4", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/if_looks_could_kill_qesda4.jpg", new TimeSpan(0, 0, 3, 14, 0), 5, new DateOnly(2023, 3, 3), 1, "if looks could kill" },
                    { new Guid("24111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/Lil_Wayne_-_Kat_Food_Visualizer_wkbtoy.mp3", "Lil_Wayne_-_Kat_Food_Visualizer_wkbtoy", "kat_food_z2ime5", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698282/kat_food_z2ime5.jpg", new TimeSpan(0, 0, 4, 46, 0), 0, new DateOnly(2023, 9, 1), 1, "Kat Food" },
                    { new Guid("25111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/MDMA_rsk7co.mp3", "MDMA_rsk7co", "xtended_wemgwk", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 48, 0), 3, new DateOnly(2022, 10, 31), 1, "MDMA" },
                    { new Guid("26111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203103/Ken_Carson_-_Freestyle_2_Official_Music_Video_toao3c.mp3", "Ken_Carson_-_Freestyle_2_Official_Music_Video_toao3c", "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 18, 0), 0, new DateOnly(2022, 10, 31), 1, "Freestyle 2" },
                    { new Guid("27111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203087/Delinquent_jhnsw8.mp3", "Delinquent_jhnsw8", "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 45, 0), 0, new DateOnly(2022, 10, 31), 1, "Delinquent" },
                    { new Guid("28111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203102/Ken_Carson_-_Fashion_Habits_Official_Audio_j3mix5.mp3", "Ken_Carson_-_Fashion_Habits_Official_Audio_j3mix5", "xtended_wemgwk", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 21, 0), 0, new DateOnly(2022, 10, 31), 1, "Fashion Habits" },
                    { new Guid("29111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203109/Ken_Carson_-_Lord_Of_Chaos_Official_Music_Video_xzpba7.mp3", "Ken_Carson_-_Lord_Of_Chaos_Official_Music_Video_xzpba7", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 1, 0), 0, new DateOnly(2025, 4, 11), 1, "Lord Of Chaos" },
                    { new Guid("30111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1776078791/Money_Spread_s3bo5n.mp3", "Money_Spread_s3bo5n", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 1, 45, 0), 0, new DateOnly(2025, 4, 11), 1, "Money Spread" },
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
                    { new Guid("1144749b-5adf-4483-a47d-71784dee32fa"), new DateOnly(2026, 3, 1), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("2eee3c05-3bd2-46fd-a856-9addff435f6d"), new DateOnly(2026, 3, 20), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("3b09c718-a2f1-407b-9065-435ef0218398"), new DateOnly(2026, 4, 5), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("4528a2c6-e74b-4290-9769-33999760f615"), new DateOnly(2026, 3, 15), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("5527b72e-dbe8-4771-af09-01f58b78b4ea"), new DateOnly(2026, 1, 22), new Guid("02111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("75b95e4e-0454-423f-8154-8e17467f1838"), new DateOnly(2026, 2, 20), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("80d50349-05b5-4cac-86aa-e830218668ea"), new DateOnly(2026, 2, 28), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("8138d428-4d9d-4664-8128-dc0ae25c67aa"), new DateOnly(2026, 1, 10), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("998e2340-33dd-449a-a8d0-c35785407e74"), new DateOnly(2026, 2, 5), new Guid("03111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("a2a5cd6c-425e-4e06-86a1-c7bd8e8cf245"), new DateOnly(2026, 1, 15), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("abfdf889-5a89-4fcb-9d3b-bcf9341af6e9"), new DateOnly(2026, 3, 25), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("b859b1ae-94d8-4e2d-a74c-09b805537883"), new DateOnly(2026, 2, 15), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("be3ced7e-3b00-4f28-88a8-6754aff3a6c3"), new DateOnly(2026, 4, 12), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("c6c904d6-613a-4b59-ab8c-1fa29431c864"), new DateOnly(2026, 4, 1), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("ce61b496-f6e0-43c0-b7cc-781d42ad2f08"), new DateOnly(2026, 4, 2), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("e42b6f4b-506e-440a-bd8e-147a89637ebf"), new DateOnly(2026, 3, 10), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("ed58bc3b-3f6d-40b5-ba12-d7fd83dab77f"), new DateOnly(2026, 1, 5), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("f74782e8-cf7d-4034-b5a7-1535c6901828"), new DateOnly(2026, 2, 10), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") }
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
