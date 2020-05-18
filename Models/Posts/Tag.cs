using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApplication.Models.Posts
{
    /*
     * This is the tag model. This will be used to get and and also set data related to blog tags.
     */
    public class Tag
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
         * This is linking the tag to the posts model, alternatively known as relationships
         */
        public IList<PostTag> Posts
        {
            get;
            set;
        }
    }
}
