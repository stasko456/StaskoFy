using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Song
{
    public class SongDetailsForMusicPlayer
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageURL { get; set; } = null!;

        public TimeSpan Duration { get; set; }

        public List<string> Artists { get; set; } = null!;

        public string AudioURL { get; set; } = null!;
    }
}
