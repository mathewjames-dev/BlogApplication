using BlogApplication.Models.Posts;
using System.Collections.Generic;

namespace BlogApplication.Models
{
    public class IndexViewModel
    {
        public IList<Post> Posts { get; set; }

        public bool IsSignedIn { get; set; }
    }
}
