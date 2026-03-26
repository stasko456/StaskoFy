using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Artist
{
    public class ArtistApprovalViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public UploadStatus IsAccepted { get; set; }
    }
}
