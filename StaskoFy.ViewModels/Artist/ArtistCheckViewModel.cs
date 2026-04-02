using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Artist
{
    public class ArtistCheckViewModel
    {
        public Guid Id { get; set; }

        public UploadStatus IsAccepted { get; set; }
    }
}
