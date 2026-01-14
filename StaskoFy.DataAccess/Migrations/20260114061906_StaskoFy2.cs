using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaskoFy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StaskoFy2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistAlbum_Album_AlbumId",
                table: "ArtistAlbum");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistAlbum_AspNetUsers_ArtistId",
                table: "ArtistAlbum");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistSong_AspNetUsers_ArtistId",
                table: "ArtistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistSong_Song_SongId",
                table: "ArtistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_LikedSongs_Song_SongId",
                table: "LikedSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_AspNetUsers_UserId",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Playlist_PlaylistId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistSong_Song_SongId",
                table: "PlaylistSong");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Album_AlbumId",
                table: "Song");

            migrationBuilder.DropForeignKey(
                name: "FK_Song_Genre_GenreId",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Song",
                table: "Song");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlist",
                table: "Playlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistSong",
                table: "ArtistSong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistAlbum",
                table: "ArtistAlbum");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Song",
                newName: "Songs");

            migrationBuilder.RenameTable(
                name: "PlaylistSong",
                newName: "PlaylistsSongs");

            migrationBuilder.RenameTable(
                name: "Playlist",
                newName: "Playlists");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "ArtistSong",
                newName: "ArtistsSongs");

            migrationBuilder.RenameTable(
                name: "ArtistAlbum",
                newName: "ArtistsAlbums");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Albums");

            migrationBuilder.RenameIndex(
                name: "IX_Song_GenreId",
                table: "Songs",
                newName: "IX_Songs_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Song_AlbumId",
                table: "Songs",
                newName: "IX_Songs_AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSong_SongId",
                table: "PlaylistsSongs",
                newName: "IX_PlaylistsSongs_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistSong_PlaylistId",
                table: "PlaylistsSongs",
                newName: "IX_PlaylistsSongs_PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_UserId",
                table: "Playlists",
                newName: "IX_Playlists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistSong_SongId",
                table: "ArtistsSongs",
                newName: "IX_ArtistsSongs_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistSong_ArtistId",
                table: "ArtistsSongs",
                newName: "IX_ArtistsSongs_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistAlbum_ArtistId",
                table: "ArtistsAlbums",
                newName: "IX_ArtistsAlbums_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistAlbum_AlbumId",
                table: "ArtistsAlbums",
                newName: "IX_ArtistsAlbums_AlbumId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Songs",
                table: "Songs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistsSongs",
                table: "PlaylistsSongs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistsSongs",
                table: "ArtistsSongs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistsAlbums",
                table: "ArtistsAlbums",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Albums",
                table: "Albums",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UserId",
                table: "Artists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistsAlbums_Albums_AlbumId",
                table: "ArtistsAlbums",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistsAlbums_Artists_ArtistId",
                table: "ArtistsAlbums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistsSongs_Artists_ArtistId",
                table: "ArtistsSongs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistsSongs_Songs_SongId",
                table: "ArtistsSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedSongs_Songs_SongId",
                table: "LikedSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistsSongs_Playlists_PlaylistId",
                table: "PlaylistsSongs",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistsSongs_Songs_SongId",
                table: "PlaylistsSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenreId",
                table: "Songs",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArtistsAlbums_Albums_AlbumId",
                table: "ArtistsAlbums");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistsAlbums_Artists_ArtistId",
                table: "ArtistsAlbums");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistsSongs_Artists_ArtistId",
                table: "ArtistsSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistsSongs_Songs_SongId",
                table: "ArtistsSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_LikedSongs_Songs_SongId",
                table: "LikedSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_AspNetUsers_UserId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistsSongs_Playlists_PlaylistId",
                table: "PlaylistsSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistsSongs_Songs_SongId",
                table: "PlaylistsSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Albums_AlbumId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenreId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Songs",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistsSongs",
                table: "PlaylistsSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistsSongs",
                table: "ArtistsSongs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArtistsAlbums",
                table: "ArtistsAlbums");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Albums",
                table: "Albums");

            migrationBuilder.RenameTable(
                name: "Songs",
                newName: "Song");

            migrationBuilder.RenameTable(
                name: "PlaylistsSongs",
                newName: "PlaylistSong");

            migrationBuilder.RenameTable(
                name: "Playlists",
                newName: "Playlist");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "ArtistsSongs",
                newName: "ArtistSong");

            migrationBuilder.RenameTable(
                name: "ArtistsAlbums",
                newName: "ArtistAlbum");

            migrationBuilder.RenameTable(
                name: "Albums",
                newName: "Album");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_GenreId",
                table: "Song",
                newName: "IX_Song_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Songs_AlbumId",
                table: "Song",
                newName: "IX_Song_AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistsSongs_SongId",
                table: "PlaylistSong",
                newName: "IX_PlaylistSong_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistsSongs_PlaylistId",
                table: "PlaylistSong",
                newName: "IX_PlaylistSong_PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_UserId",
                table: "Playlist",
                newName: "IX_Playlist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistsSongs_SongId",
                table: "ArtistSong",
                newName: "IX_ArtistSong_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistsSongs_ArtistId",
                table: "ArtistSong",
                newName: "IX_ArtistSong_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistsAlbums_ArtistId",
                table: "ArtistAlbum",
                newName: "IX_ArtistAlbum_ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistsAlbums_AlbumId",
                table: "ArtistAlbum",
                newName: "IX_ArtistAlbum_AlbumId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Song",
                table: "Song",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistSong",
                table: "PlaylistSong",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlist",
                table: "Playlist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistSong",
                table: "ArtistSong",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArtistAlbum",
                table: "ArtistAlbum",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistAlbum_Album_AlbumId",
                table: "ArtistAlbum",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistAlbum_AspNetUsers_ArtistId",
                table: "ArtistAlbum",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSong_AspNetUsers_ArtistId",
                table: "ArtistSong",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistSong_Song_SongId",
                table: "ArtistSong",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikedSongs_Song_SongId",
                table: "LikedSongs",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_AspNetUsers_UserId",
                table: "Playlist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Playlist_PlaylistId",
                table: "PlaylistSong",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistSong_Song_SongId",
                table: "PlaylistSong",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Album_AlbumId",
                table: "Song",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Genre_GenreId",
                table: "Song",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
