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
                    { new Guid("01111111-1111-1111-1111-111111111111"), 0, "ken_carson_amg20i", "d01d0510-9c2d-4f03-bb8b-3187eb3dbf85", "kenCarson@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/ken_carson_amg20i.jpg", false, null, "kenCarson@gmail.com", "kenCarson", "AQAAAAIAAYagAAAAEKYqCMIGj2hh64uEzQtFn3WmCgTeJlFUPDW07CSIPlHPBxHsKmG4BTAZSUqi1tOQpw==", null, false, "01111111-1111-1111-1111-111111111111", false, "kenCarson" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), 0, "future_pbmahw", "c5f6dc23-c8ca-4907-8ab3-2fcaad29be9a", "future@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/future_pbmahw.jpg", false, null, "future@gmail.com", "future", "AQAAAAIAAYagAAAAEO362dR9hVD4te4EludeYVXNvD5AnQ5Pz/g4Ty5m4yMVzR9BYN8T0DpntsLX12CQVQ==", null, false, "02111111-1111-1111-1111-111111111111", false, "future" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), 0, "young_thug_wz2fln", "1fa8f3de-9581-4b13-870b-47d98778ccf6", "youngThug@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698511/young_thug_wz2fln.jpg", false, null, "youngThug@gmail.com", "youngThug", "AQAAAAIAAYagAAAAENW5iCoyfoiDpqDEPKZtQ5GYNodH01ijMrMVUQLCiro5vW6rUZ5PMSizyb3smVwoKw==", null, false, "03111111-1111-1111-1111-111111111111", false, "youngThug" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), 0, "westside_gunn_vm7xf2", "ed84e97b-e910-43a5-95d8-10bdbdc93017", "westsideGunn@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698510/westside_gunn_vm7xf2.jpg", false, null, "westsideGunn@gmail.com", "westsideGunn", "AQAAAAIAAYagAAAAEAL09OjIPI0F/xdyvtQ5N7T+poeQuoLUW/FHoIRre147A2eqOYSGPsTTwh1fAK/A2g==", null, false, "04111111-1111-1111-1111-111111111111", false, "westsideGunn" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), 0, "tyler_the_creator_i9yhnu", "6b6f8678-280f-4259-ae17-b58b1ae8fbae", "tylerTheCreator@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698509/tyler_the_creator_i9yhnu.jpg", false, null, "tylerTheCreator@gmail.com", "tylerTheCreator", "AQAAAAIAAYagAAAAEKsnCWJW700TCClrBG6z63l9Covix0l9/9ic9wWpM/0E1syBg+jjvj8JeeaR8DNQFQ==", null, false, "05111111-1111-1111-1111-111111111111", false, "tylerTheCreator" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), 0, "destroy_lonely_hmhymx", "cdff4fc3-74f7-40ef-969c-dca648aa9ed7", "destroyLonely@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698502/destroy_lonely_hmhymx.jpg", false, null, "destroyLonely@gmail.com", "destroyLonely", "AQAAAAIAAYagAAAAEJbs5nOLS8cxFJtYgFy6TC2sQexUzzJ5csbvY/XYpidh46jKOoWm0Z05onjR7q9z2g==", null, false, "06111111-1111-1111-1111-111111111111", false, "destroyLonely" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), 0, "joey_bada_t4ig6u", "c70ce46e-e383-455a-80d4-e0df3c4ededc", "joeyBada$$@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698505/joey_bada_t4ig6u.jpg", false, null, "joeyBada$$@gmail.com", "joeyBada$$", "AQAAAAIAAYagAAAAEKZdUBxUIkAip9kxtBufnS0/G+RNsxSZRmswkJGzrb2MzKTy2xVwcTr+rQa9bMGDwQ==", null, false, "07111111-1111-1111-1111-111111111111", false, "joeyBada$$" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), 0, "billie_essco_tqdmip", "13f5970a-53d5-4e72-8e71-529c42878c47", "billiEssco@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698501/billie_essco_tqdmip.jpg", false, null, "billiEssco@gmail.com", "billiEssco", "AQAAAAIAAYagAAAAEJDquqIzbaERzm+FFKuzoXuBLn6sPd4i+By9wamSY7Lvlo63eHmFvcz8EtFzuo2sBA==", null, false, "08111111-1111-1111-1111-111111111111", false, "billiEssco" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), 0, "lil_wayne_pqbiny", "9f558e6c-df61-4cdc-8e97-08ee3062f087", "lilWayne@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/lil_wayne_pqbiny.jpg", false, null, "lilWayne@gmail.com", "lilWayne", "AQAAAAIAAYagAAAAEPL8nVHvMDhqr9aCcZTG84PXisKzsJEFy3aF6C6jPZ4nHl+zAznK/GVAi1nZpXK8sA==", null, false, "09111111-1111-1111-1111-111111111111", false, "lilWayne" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), 0, "homixide_gang_anf7iv", "f1241359-47bb-4c7b-a776-68ed710d3c1e", "homixideGang@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698504/homixide_gang_anf7iv.jpg", false, null, "homixideGang@gmail.com", "homixideGang", "AQAAAAIAAYagAAAAEMYIVYI8MYUxEofAab7k3Dn6NZ6Qg90MpQNdqcEhRwo0wuVG1Nvi4FW/mzG0dsrbLw==", null, false, "10111111-1111-1111-1111-111111111111", false, "homixideGang" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "stasko456_hblwlq", "31f68a70-312b-4152-9f18-e25001dbeeec", "stdimov2007@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/stasko456_hblwlq.jpg", false, null, "stdimov2007@gmail.com", "stasko456", "AQAAAAIAAYagAAAAEEisla880p2wVyUAvBn2eRarZLfam4Octlfc4caKkLFFSEtFBxA0l6UHTdSJgSbdag==", null, false, "11111111-1111-1111-1111-111111111111", false, "stasko456" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), 0, "simon333_fafgdv", "07de7d2b-e44d-4672-8e6b-567e6b15862e", "simon2403e8@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/simon333_fafgdv.jpg", false, null, "simon2403e8@gmail.com", "simon333", "AQAAAAIAAYagAAAAEIEePhUOKngJ1vY8YZXv4xQGILXhUzqsF+DNyvW0izUGQgc9Uh1rV9BjLOP9fwlsgQ==", null, false, "12111111-1111-1111-1111-111111111111", false, "simon333" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), 0, "n_peew07_yoj6ay", "03f577ea-6cdc-4576-b1e9-6749af122813", "nikolaPeew@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698507/n_peew07_yoj6ay.jpg", false, null, "nikolaPeew@gmail.com", "n_peew07", "AQAAAAIAAYagAAAAEIUNsX1V6nm64GIUUtPwW5R8NjmWxykXqI1U7uG7WADY7bBM85S2muT5hwk5nttrhQ==", null, false, "13111111-1111-1111-1111-111111111111", false, "n_peew07" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), 0, "gt_baby_gdk5le", "792bffa7-09e9-472f-b9dd-9a1cce47003d", "gtonev@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/gt_baby_gdk5le.jpg", false, null, "gtonev@gmail.com", "g_tonev", "AQAAAAIAAYagAAAAEIwzfGXq9oxGEK9Axml5hc5LETRW2YCiwEUzViBP+BVe8YNTbYbFFQrXsEhPcWed7g==", null, false, "14111111-1111-1111-1111-111111111111", false, "g_tonev" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), 0, "adasha_quhjni", "df9ab2be-7c8f-4373-828c-ffc2a6a4c399", "nikolaGragov@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1773127136/adasha_quhjni.png", false, null, "nikolaGragov@gmail.com", "niksy_g", "AQAAAAIAAYagAAAAEGy8Qz4sDQqiOGmYqqcPQDk6ch7GUHdN8sqP316BpoKvjWZyHTQ8DYM7JMdoY69Gjw==", null, false, "15111111-1111-1111-1111-111111111111", false, "niksy_g" }
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
                columns: new[] { "Id", "AlbumId", "AudioURL", "CloudinaryAudioPublicId", "CloudinaryPublicId", "GenreId", "ImageURL", "Length", "Likes", "ReleaseDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203109/Ken_Carson_-_Green_Room_Official_Audio_cksiai.mp3", "Ken_Carson_-_Green_Room_Official_Audio_cksiai", "a_great_chaos_deluxe_d2vxhf", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 3, 8, 0), 6, new DateOnly(2024, 7, 5), 1, "Green Room" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203106/Ken_Carson_-_Lose_It_Official_Audio_l2wjgb.mp3", "Ken_Carson_-_Lose_It_Official_Audio_l2wjgb", "a_great_chaos_deluxe_d2vxhf", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 2, 20, 0), 3, new DateOnly(2024, 7, 5), 1, "Lose It" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_-_Me_N_My_Kup_Official_Audio_hfuxfy.mp3", "Ken_Carson_-_Me_N_My_Kup_Official_Audio_hfuxfy", "a_great_chaos_deluxe_d2vxhf", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 3, 54, 0), 2, new DateOnly(2024, 7, 5), 1, "Me N My Kup" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_Destroy_Lonely_-_Paranoid_Official_Audio_axsypt.mp3", "Ken_Carson_Destroy_Lonely_-_Paranoid_Official_Audio_axsypt", "a_great_chaos_deluxe_d2vxhf", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 2, 7, 0), 4, new DateOnly(2024, 7, 5), 1, "Paranoid" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203108/Ken_Carson_-_ss_Official_Audio_v0ttsl.mp3", "Ken_Carson_-_ss_Official_Audio_v0ttsl", "a_great_chaos_deluxe_d2vxhf", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/a_great_chaos_deluxe_d2vxhf.jpg", new TimeSpan(0, 0, 3, 4, 0), 0, new DateOnly(2024, 7, 5), 1, "ss" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203094/Future_Young_Thug_-_No_Cap_Official_Audio_vguatf.mp3", "Future_Young_Thug_-_No_Cap_Official_Audio_vguatf", "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 24, 0), 3, new DateOnly(2017, 10, 20), 1, "No Cap" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203100/Future_Young_Thug_-_All_Da_Smoke_Official_Audio_qxk6gu.mp3", "Future_Young_Thug_-_All_Da_Smoke_Official_Audio_qxk6gu", "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 3, 24, 0), 0, new DateOnly(2017, 10, 20), 1, "All da Smoke" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Future_Young_Thug_-_200_Official_Audio_olbfa7.mp3", "Future_Young_Thug_-_200_Official_Audio_olbfa7", "super_slimey_v5r2c1", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 26, 0), 5, new DateOnly(2017, 10, 20), 1, "200" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203126/Young_Thug_-_Cruise_Ship_Official_Audio_vbju6d.mp3", "Young_Thug_-_Cruise_Ship_Official_Audio_vbju6d", "super_slimey_v5r2c1", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 46, 0), 0, new DateOnly(2017, 10, 20), 1, "Cruise Ship" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Future_-_Feed_Me_Dope_Official_Audio_ewzphn.mp3", "Future_-_Feed_Me_Dope_Official_Audio_ewzphn", "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 2, 46, 0), 0, new DateOnly(2017, 10, 20), 1, "Feed Me Dope" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203129/Young_Thug_-_Killed_Before_Official_Audio_zjwb5d.mp3", "Young_Thug_-_Killed_Before_Official_Audio_zjwb5d", "super_slimey_v5r2c1", new Guid("21111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/super_slimey_v5r2c1.jpg", new TimeSpan(0, 0, 3, 40, 0), 0, new DateOnly(2017, 10, 20), 1, "Killed Before" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203123/Westside_Gunn_-_No_Vacancy_Audio_bq6czj.mp3", "Westside_Gunn_-_No_Vacancy_Audio_bq6czj", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 1, 35, 0), 0, new DateOnly(2020, 4, 17), 1, "No Vacancy" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203089/327_feat._Tyler_The_Creator_Billie_Essco_r4lgdt.mp3", "327_feat._Tyler_The_Creator_Billie_Essco_r4lgdt", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 5, 49, 0), 0, new DateOnly(2020, 4, 17), 1, "327" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203089/Euro_Step_rdmpx9.mp3", "Euro_Step_rdmpx9", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 1, 49, 0), 0, new DateOnly(2020, 4, 17), 1, "Euro Step" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203135/Versace_hesjgp.mp3", "Versace_hesjgp", "pray_for_paris_rx4tq8", new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698283/pray_for_paris_rx4tq8.jpg", new TimeSpan(0, 0, 2, 4, 0), 0, new DateOnly(2020, 4, 17), 1, "Versace" },
                    { new Guid("16111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203100/HOT_WIND_BLOWS_Audio_yldqrr.mp3", "HOT_WIND_BLOWS_Audio_yldqrr", "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 2, 35, 0), 0, new DateOnly(2023, 3, 31), 1, "HOT WIND BLOWS" },
                    { new Guid("17111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203130/WILSHIRE_Audio_z3sjfq.mp3", "WILSHIRE_Audio_z3sjfq", "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 8, 35, 0), 0, new DateOnly(2023, 3, 31), 1, "WILSHIRE" },
                    { new Guid("18111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203119/SAFARI_Audio_ezvcpf.mp3", "SAFARI_Audio_ezvcpf", "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 2, 57, 0), 0, new DateOnly(2023, 3, 31), 1, "SAFARI" },
                    { new Guid("19111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203127/WHAT_A_DAY_lxaxt0.mp3", "WHAT_A_DAY_lxaxt0", "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 3, 36, 0), 0, new DateOnly(2023, 3, 31), 1, "WHAT A DAY" },
                    { new Guid("20111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203123/Tyler_The_Creator_-_DOGTOOTH_zrwqrg.mp3", "Tyler_The_Creator_-_DOGTOOTH_zrwqrg", "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 2, 41, 0), 0, new DateOnly(2023, 3, 31), 1, "DOGTOOTH" },
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203101/HEAVEN_TO_ME_vfsw3y.mp3", "HEAVEN_TO_ME_vfsw3y", "call_me_if_you_get_lost_the_estate_sale_xiqapi", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/call_me_if_you_get_lost_the_estate_sale_xiqapi.jpg", new TimeSpan(0, 0, 3, 50, 0), 0, new DateOnly(2023, 3, 31), 1, "HEAVEN TO ME" },
                    { new Guid("22111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203086/Bane_loijws.mp3", "Bane_loijws", "bane_v4s8f8", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698280/bane_v4s8f8.jpg", new TimeSpan(0, 0, 2, 20, 0), 0, new DateOnly(2019, 7, 30), 1, "Bane" },
                    { new Guid("23111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203117/Destroy_Lonely_-_if_looks_could_kill_Official_Audio_rpq9dr.mp3", "Destroy_Lonely_-_if_looks_could_kill_Official_Audio_rpq9dr", "if_looks_could_kill_qesda4", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698281/if_looks_could_kill_qesda4.jpg", new TimeSpan(0, 0, 3, 14, 0), 5, new DateOnly(2023, 3, 3), 1, "if looks could kill" },
                    { new Guid("24111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/Lil_Wayne_-_Kat_Food_Visualizer_wkbtoy.mp3", "Lil_Wayne_-_Kat_Food_Visualizer_wkbtoy", "kat_food_z2ime5", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698282/kat_food_z2ime5.jpg", new TimeSpan(0, 0, 4, 46, 0), 0, new DateOnly(2023, 9, 1), 1, "Kat Food" },
                    { new Guid("25111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/MDMA_rsk7co.mp3", "MDMA_rsk7co", "xtended_wemgwk", new Guid("31111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 48, 0), 3, new DateOnly(2022, 10, 31), 1, "MDMA" },
                    { new Guid("26111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203103/Ken_Carson_-_Freestyle_2_Official_Music_Video_toao3c.mp3", "Ken_Carson_-_Freestyle_2_Official_Music_Video_toao3c", "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 18, 0), 0, new DateOnly(2022, 10, 31), 1, "Freestyle 2" },
                    { new Guid("27111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203087/Delinquent_jhnsw8.mp3", "Delinquent_jhnsw8", "xtended_wemgwk", new Guid("41111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 2, 45, 0), 0, new DateOnly(2022, 10, 31), 1, "Delinquent" },
                    { new Guid("28111111-1111-1111-1111-111111111111"), new Guid("51111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203102/Ken_Carson_-_Fashion_Habits_Official_Audio_j3mix5.mp3", "Ken_Carson_-_Fashion_Habits_Official_Audio_j3mix5", "xtended_wemgwk", new Guid("11111111-1111-1111-1111-111111111111"), "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698284/xtended_wemgwk.jpg", new TimeSpan(0, 0, 3, 21, 0), 0, new DateOnly(2022, 10, 31), 1, "Fashion Habits" },
                    { new Guid("29111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203109/Ken_Carson_-_Lord_Of_Chaos_Official_Music_Video_xzpba7.mp3", "Ken_Carson_-_Lord_Of_Chaos_Official_Music_Video_xzpba7", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 1, 0), 0, new DateOnly(2025, 4, 11), 1, "Lord Of Chaos" },
                    { new Guid("30111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203113/Money_Spread_p2e3go.mp3", "Money_Spread_p2e3go", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 1, 45, 0), 0, new DateOnly(2025, 4, 11), 1, "Money Spread" },
                    { new Guid("31111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203119/Trap_Jump_eb9qg6.mp3", "Trap_Jump_eb9qg6", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 2, 30, 0), 0, new DateOnly(2025, 4, 11), 1, "Trap Jump" },
                    { new Guid("32111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203090/Blakk_Rokkstar_orlufo.mp3", "Blakk_Rokkstar_orlufo", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 50, 0), 0, new DateOnly(2025, 4, 11), 1, "Blakk Rokkstar" },
                    { new Guid("33111111-1111-1111-1111-111111111111"), null, "https://res.cloudinary.com/stasko456cloud/video/upload/v1775203114/LiveLeak_hkoc3w.mp3", "LiveLeak_hkoc3w", "", new Guid("41111111-1111-1111-1111-111111111111"), "/images/defaults/default-song-cover-art.png", new TimeSpan(0, 0, 3, 10, 0), 0, new DateOnly(2025, 4, 11), 1, "LiveLeak" },
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
                    { new Guid("01242a1b-19ff-4033-8ad7-2438ba80fda2"), new DateOnly(2026, 4, 12), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("0cc06119-3516-4dad-9f1b-0eb19240ac7c"), new DateOnly(2026, 3, 25), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("18130ee3-0b93-44b6-95e0-6645018e4b4e"), new DateOnly(2026, 4, 2), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("1cd66ca8-6a1f-4ecd-971e-3f0bbf99d96c"), new DateOnly(2026, 2, 20), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("39bc2795-3e5e-4f9c-b512-febd43f8df08"), new DateOnly(2026, 3, 1), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("3bbc4f8f-2f75-4cfd-8087-023e8d3de5f7"), new DateOnly(2026, 2, 15), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("41f31d61-fdd3-4e54-a5b4-34d5d2f0315a"), new DateOnly(2026, 1, 5), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("54dbfad3-2739-4646-b120-4b1af74b0423"), new DateOnly(2026, 1, 10), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("655053ac-3066-45b6-aa26-07d46ca66fe0"), new DateOnly(2026, 2, 5), new Guid("03111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("91a4c97e-fa08-418f-b6a2-185551edd352"), new DateOnly(2026, 3, 15), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("b0bc1362-bded-4fdd-a8bd-fd52387b4c8c"), new DateOnly(2026, 1, 22), new Guid("02111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("b8dd3699-6241-4260-a1af-b47d8b48405d"), new DateOnly(2026, 2, 28), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("bb537c3f-3668-4910-b2b1-357cbf0eb127"), new DateOnly(2026, 3, 20), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("cf0e542f-2b51-40eb-99c5-9de5ddaa0683"), new DateOnly(2026, 1, 15), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("cfd5a210-a4ec-4771-98d2-b44e1a29a770"), new DateOnly(2026, 4, 1), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("dd4d52f6-4515-45b5-b9ec-232350a64d68"), new DateOnly(2026, 4, 5), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("e39cae4e-cfbc-437f-8962-01320662217e"), new DateOnly(2026, 3, 10), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("e4df147e-e404-477d-a13d-cacd26253182"), new DateOnly(2026, 2, 10), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") }
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
