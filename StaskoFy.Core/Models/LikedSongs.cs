using StaskoFy.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.Core.Models
{
    public class LikedSongs
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid SongId { get; set; }
        public Song Song { get; set; }

        public DateOnly DateAdded { get; set; }
    }
}
