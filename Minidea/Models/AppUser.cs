﻿using Microsoft.AspNetCore.Identity;

namespace Minidea.Models
{
    public class AppUser : IdentityUser
    {

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }
}
