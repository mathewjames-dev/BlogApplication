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
    public class Post
    {
        public int Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string ShortDescription
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string MetaTitle
        {
            get;
            set;
        }

        public string MetaDescription
        {
            get;
            set;
        }

        public string UrlSlug
        {
            get;
            set;
        }

        public bool Published
        {
            get;
            set;
        }

        public DateTime PostedOn
        {
            get;
            set;
        }

        public DateTime? Modified
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        /*
         * This is linking the Post to the Category model, alternatively known as relationships
         * But we still want to setup a setter and getter for the category id on the post table.
         */
        public int CategoryId
        {
            get;
            set;
        }
        public Category Category
        {
            get;
            set;
        }

        /*
         * This is linking the Post to the Tags via a pivot table, alternatively known as relationships too.
         */
        public IList<PostTag> PostTags
        {
            get;
            set;
        }
    }
}
