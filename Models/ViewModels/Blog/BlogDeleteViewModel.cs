using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace BlogApplication.Models
{
    public class BlogDeleteViewModel
    {
        public Post Post { get; set; }
    }
}
