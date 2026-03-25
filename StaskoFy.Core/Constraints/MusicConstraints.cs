using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Constraints
{
    public static class MusicConstraints
    {
        public static int MaxArtistsPerProject = 5;
        public static int MaxSongsPerAlbum = 50;
    }
}
