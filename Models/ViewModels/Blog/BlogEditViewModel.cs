using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace BlogApplication.Models
{
    public class BlogEditViewModel
    {
        public Post Post { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
