using StaskoFy.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.Genre
{
    public class GenreApprovalViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public UploadStatus Status { get; set; }
    }
}
