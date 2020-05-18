using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApplication.Models.Posts
{
    /*
     * This is the category model. This will be used to get and and also set data related to blog categories.
     * Majority of the properties will be self-explanatory and properties we would normally associate with blog categories.
     */
    public class Category
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string UrlSlug
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        /*
         * This is linking the Categories to the Posts model, alternatively known as relationships
         */
        public IList<Post> CategoryPosts
        {
            get;
            set;
        }
    }
}
