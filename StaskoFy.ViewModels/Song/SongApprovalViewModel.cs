using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongApprovalViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;


        public string Genre { get; set; } = null!;

        public UploadStatus Status { get; set; }

        public List<string> Artists { get; set; } = null!;
    }
}
