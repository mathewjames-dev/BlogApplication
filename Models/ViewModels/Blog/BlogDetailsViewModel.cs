using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace BlogApplication.Models
{
    public class BlogDetailsViewModel
    {
        public Post Post { get; set; }

        public string ModifiedDate { get; set; }

        public IList<Post> RelatedPosts { get; set; }
    }
}
