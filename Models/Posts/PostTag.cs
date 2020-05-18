using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApplication.Models.Posts
{
    /*
     * This is the post model. This will be used to get and and also set data related to blog posts.
     * Majority of the properties will be self-explanatory and properties we would normally associate with blog posts.
     */
    public class PostTag
    {
        public int PostId
        {
            get;
            set;
        }

        /*
         * This is linking the posttag to the post model, alternatively known as relationships
         */
        public Post Post
        {
            get;
            set;
        }

        public int TagId
        {
            get;
            set;
        }

        /*
         * This is linking the posttag to the tag model, alternatively known as relationships
         */
        public Tag Tag
        {
            get;
            set;
        }
    }
}
