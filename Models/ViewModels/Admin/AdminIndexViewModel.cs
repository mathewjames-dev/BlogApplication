using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BlogApplication.Models
{
    public class AdminIndexViewModel
    {
        public IList<IdentityUser> Users { get; set; }
    }
}
