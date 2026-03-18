using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.User
{
    public class UserIndexViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;
    }
}
