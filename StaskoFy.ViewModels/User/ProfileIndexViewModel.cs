using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoFy.ViewModels.User
{
    public class ProfileIndexViewModel
    {
        public string Username { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;
    }
}
