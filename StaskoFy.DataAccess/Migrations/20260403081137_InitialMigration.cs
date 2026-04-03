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
                    { new Guid("01111111-1111-1111-1111-111111111111"), 0, "ken_carson_amg20i", "6c49e3c8-7359-48bb-b25e-5eda7c4250fd", "kenCarson@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/ken_carson_amg20i.jpg", false, null, "kenCarson@gmail.com", "kenCarson", "AQAAAAIAAYagAAAAEG8DPZkFiyMRdUNWgmkeH02mYY7ML89XMTAXZwB78qZqOZs5jaa1sm9/FUaRKNVAiw==", null, false, "01111111-1111-1111-1111-111111111111", false, "kenCarson" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), 0, "future_pbmahw", "b030f913-6788-4bff-8411-d7d9dd4b5e60", "future@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/future_pbmahw.jpg", false, null, "future@gmail.com", "future", "AQAAAAIAAYagAAAAECPGsSiX2L3rbOamXz5ivlYywFZiUVPigm3PDgMJkgEF6McbcOK5Y2GmorvRdfgNNA==", null, false, "02111111-1111-1111-1111-111111111111", false, "future" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), 0, "young_thug_wz2fln", "692558bb-f95a-46a6-8d4c-fca4f3b9cb77", "youngThug@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698511/young_thug_wz2fln.jpg", false, null, "youngThug@gmail.com", "youngThug", "AQAAAAIAAYagAAAAEMKyCUP8Erf5xkXud8WRIAM0hY6PgFyR6wrrRK1A1e3SOKVUhGLi784Un8dq4Mg1GQ==", null, false, "03111111-1111-1111-1111-111111111111", false, "youngThug" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), 0, "westside_gunn_vm7xf2", "9dcfe43f-fe53-4135-ac46-26b85f767cd7", "westsideGunn@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698510/westside_gunn_vm7xf2.jpg", false, null, "westsideGunn@gmail.com", "westsideGunn", "AQAAAAIAAYagAAAAEMseItwCYJoruk/iTQ97qLxCYAjHNNry7LkR5sqh5dxFweKUEl1LPuW3OTknGbMa6w==", null, false, "04111111-1111-1111-1111-111111111111", false, "westsideGunn" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), 0, "tyler_the_creator_i9yhnu", "d756419e-dccc-4b92-9795-ea959a6d82d2", "tylerTheCreator@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698509/tyler_the_creator_i9yhnu.jpg", false, null, "tylerTheCreator@gmail.com", "tylerTheCreator", "AQAAAAIAAYagAAAAEF1NOIPJe3pkXnblwjGUfD58xT2NjZ0CZKrIAkDsttF7ixsF7O7wJE0oeYNV03rgdg==", null, false, "05111111-1111-1111-1111-111111111111", false, "tylerTheCreator" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), 0, "destroy_lonely_hmhymx", "8bfb954d-1c1c-4dba-897e-cfb5d7c9a507", "destroyLonely@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698502/destroy_lonely_hmhymx.jpg", false, null, "destroyLonely@gmail.com", "destroyLonely", "AQAAAAIAAYagAAAAEB43OLNlbLNn2NvvoM15nPC5Zj+H032d8mi9a0+TPuNYV1MVRLWuSMTbce0xDyl67Q==", null, false, "06111111-1111-1111-1111-111111111111", false, "destroyLonely" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), 0, "joey_bada_t4ig6u", "69288666-e67a-4e9e-8355-b14e2c4a2875", "joeyBada$$@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698505/joey_bada_t4ig6u.jpg", false, null, "joeyBada$$@gmail.com", "joeyBada$$", "AQAAAAIAAYagAAAAED0Qs0wvLaTdcScmt1gJPC3gmWMno3C4UJ7Jy8dYYBIv/FldapjLis9bfpPl76nM/w==", null, false, "07111111-1111-1111-1111-111111111111", false, "joeyBada$$" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), 0, "billie_essco_tqdmip", "d692cf30-9390-4178-a227-c7ae6803f89a", "billiEssco@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698501/billie_essco_tqdmip.jpg", false, null, "billiEssco@gmail.com", "billiEssco", "AQAAAAIAAYagAAAAECFdXJ1FAPpBprEnz6CRpxnXgrMjwpIJVIFtYGR5S0f7MDphycyo/i4pOJHsWLcToQ==", null, false, "08111111-1111-1111-1111-111111111111", false, "billiEssco" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), 0, "lil_wayne_pqbiny", "6863b648-20cd-4853-82b4-bd266b589a48", "lilWayne@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698506/lil_wayne_pqbiny.jpg", false, null, "lilWayne@gmail.com", "lilWayne", "AQAAAAIAAYagAAAAEDgh/xiHwVIclYXHKhEn6vXEKJLt1mAD0wQf1tV3hPp7aziqipneFAefLSrbsqtj7g==", null, false, "09111111-1111-1111-1111-111111111111", false, "lilWayne" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), 0, "homixide_gang_anf7iv", "724645dc-1069-4088-9b7b-32669559369a", "homixideGang@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698504/homixide_gang_anf7iv.jpg", false, null, "homixideGang@gmail.com", "homixideGang", "AQAAAAIAAYagAAAAEMVAuan1w5fNc47ziNajsqE30Ofy+XYABdypiNU4V6/6ak6jUuqreFt1ZsDch3om3A==", null, false, "10111111-1111-1111-1111-111111111111", false, "homixideGang" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, "stasko456_hblwlq", "91017de4-adbb-4cb1-b4cf-88e835eca9f3", "stdimov2007@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/stasko456_hblwlq.jpg", false, null, "stdimov2007@gmail.com", "stasko456", "AQAAAAIAAYagAAAAEAZ3uj+srfDi6PUm+8H8mqXUknVBTItTdfSCljXEVQG+oAL694a2qOX8bT9xjzfv6g==", null, false, "11111111-1111-1111-1111-111111111111", false, "stasko456" },
                    { new Guid("12111111-1111-1111-1111-111111111111"), 0, "simon333_fafgdv", "5b965527-8c0e-4e4e-9fe5-4cf60d14ef9d", "simon2403e8@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698508/simon333_fafgdv.jpg", false, null, "simon2403e8@gmail.com", "simon333", "AQAAAAIAAYagAAAAEEQLsva8lO115E/9aWzy8ufqvqspVI5EpNNTS/mMCciKnFl6b7siaiDRpxKpS6g2Pg==", null, false, "12111111-1111-1111-1111-111111111111", false, "simon333" },
                    { new Guid("13111111-1111-1111-1111-111111111111"), 0, "n_peew07_yoj6ay", "43f2b34f-d64f-49ea-8d35-d4697b353e27", "nikolaPeew@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698507/n_peew07_yoj6ay.jpg", false, null, "nikolaPeew@gmail.com", "n_peew07", "AQAAAAIAAYagAAAAEIlmtXPVkw5PtfaguOB/PgBg7/1rkrWbz78e7Hww3LbUwIIZrLIhM2fNm0thH8vebw==", null, false, "13111111-1111-1111-1111-111111111111", false, "n_peew07" },
                    { new Guid("14111111-1111-1111-1111-111111111111"), 0, "gt_baby_gdk5le", "7243784b-9716-40fb-b274-b3bce9117ae1", "gtonev@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1772698503/gt_baby_gdk5le.jpg", false, null, "gtonev@gmail.com", "g_tonev", "AQAAAAIAAYagAAAAEPdcLMvUmDwg65u1MAnXJT00V/OR0kK0Ap481hwKePkhw6OkRAsChaxlPk8+G6ZR+g==", null, false, "14111111-1111-1111-1111-111111111111", false, "g_tonev" },
                    { new Guid("15111111-1111-1111-1111-111111111111"), 0, "adasha_quhjni", "c786f02c-2508-4dd8-b7bb-a91dedb6281e", "nikolaGragov@gmail.com", false, "https://res.cloudinary.com/stasko456cloud/image/upload/v1773127136/adasha_quhjni.png", false, null, "nikolaGragov@gmail.com", "niksy_g", "AQAAAAIAAYagAAAAEFMHEQWCfTJ/TSHj/do337bF9ab4GFcDT5cBuv2Veic7iKFmzj2LkdvSbf+rqbY1jg==", null, false, "15111111-1111-1111-1111-111111111111", false, "niksy_g" }
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
                    { new Guid("0165d4a5-a21e-4c5c-9dd4-1ff3e9c03b15"), new DateOnly(2026, 1, 22), new Guid("02111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("0ffbb4c1-8261-4630-8f1c-0f19ab5e3f9e"), new DateOnly(2026, 4, 12), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("456099a1-b2cd-4c54-93f7-6e970bb04b66"), new DateOnly(2026, 4, 1), new Guid("34111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("52de845c-2eda-461d-8989-558469cc45f6"), new DateOnly(2026, 3, 1), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("54c635d4-c6ea-4dd6-8d1e-3a0749de2f82"), new DateOnly(2026, 4, 2), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("5c01523e-adbf-40b4-a269-9a4c4b94ce32"), new DateOnly(2026, 1, 5), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("63ca6ede-8ef7-493d-9973-5db7ef8de84d"), new DateOnly(2026, 3, 15), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("77ae65a2-5d87-4129-a0dd-143730efb09a"), new DateOnly(2026, 3, 10), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("808024dd-e4ed-4834-86ff-12a54408f492"), new DateOnly(2026, 3, 25), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("887422d4-7fa2-49f2-98b2-b3213e992816"), new DateOnly(2026, 4, 5), new Guid("25111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("8a32e25f-1b75-44c8-8392-da796abb5241"), new DateOnly(2026, 1, 10), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("8d5b10bc-335f-4482-9995-a94e29a877f9"), new DateOnly(2026, 2, 15), new Guid("06111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("cabadc6d-5fce-492a-845a-f613cf71de78"), new DateOnly(2026, 2, 5), new Guid("03111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("d5e30ea6-bf19-46fa-9ede-e609e5305066"), new DateOnly(2026, 3, 20), new Guid("23111111-1111-1111-1111-111111111111"), new Guid("01111111-1111-1111-1111-111111111111") },
                    { new Guid("d7dc3533-bd7d-4642-a34b-b25ba669534a"), new DateOnly(2026, 2, 20), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") },
                    { new Guid("efa54ecb-aadd-40d4-8045-8b7667b22007"), new DateOnly(2026, 2, 10), new Guid("04111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("f1a9cf6c-6b87-4d15-b3c2-183d5ea36afe"), new DateOnly(2026, 2, 28), new Guid("08111111-1111-1111-1111-111111111111"), new Guid("02111111-1111-1111-1111-111111111111") },
                    { new Guid("fd2714ea-9b4b-4e13-8b95-d3960e50fff8"), new DateOnly(2026, 1, 15), new Guid("01111111-1111-1111-1111-111111111111"), new Guid("03111111-1111-1111-1111-111111111111") }
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
