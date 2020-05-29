using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogApplication.Data;
using BlogApplication.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using BlogApplication.Models;
using System.Diagnostics;
using ObjectDumper;

namespace BlogApplication.Controllers
{
    [Authorize(Policy = "RequireAdminRights")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _DB;

        public BlogController(ApplicationDbContext context)
        {
            _DB = context;
        }

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            /*
             * Continuing with view models and we setup our blog index view model
             */
            BlogIndexViewModel viewModel = new BlogIndexViewModel()
            {
                Posts = await _DB.Posts.ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Blog/{slug}
        public async Task<IActionResult> Details(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            var post = await _DB.Posts
                .FirstOrDefaultAsync(m => m.UrlSlug == slug);

            if (post == null)
            {
                return NotFound();
            }

            DateTime Modified = new DateTime();
            Modified = Convert.ToDateTime(post.Modified);
            ViewData["modified_date"] = Modified.ToLongDateString();

            var relatedPosts = await _DB.Posts.Where(m => m.Id != post.Id)
                .ToListAsync();
            ViewData["related_posts"] = relatedPosts;

            return View(post);
        }

        // GET: Blog/Create
        public IActionResult Create()
        {
            /*
             * Setup the blog create view model
             */
            BlogCreateViewModel viewModel = new BlogCreateViewModel()
            {
                Post = new Post(),
                /*
                 * Set up the categories from the database to a select list item.
                 */
                Categories = (IEnumerable<SelectListItem>)_DB.Categories.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name,
                    Selected = m.Id == 1
                })
            };

            return View(viewModel);
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateViewModel viewModel)
        {
            /*
             * Add the post data from the view model into the posts table as a new record
             */
            _DB.Posts.Add(viewModel.Post);

            /*
             * Update the database changes
             */
            await _DB.SaveChangesAsync();


            /*
             * Then redirect the user back to the Index function
             */
            return RedirectToAction(nameof(Index));
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /*
             * We double check an id has been passed
             * Otherwise we return not found.
             */
            if (id == null)
            {
                return NotFound();
            }

            /*
             * We setup the blog edit view model.
             * Then we get the post from the database by id
             */
            BlogEditViewModel viewModel = new BlogEditViewModel()
            {
                Post = await _DB.Posts.FindAsync(id),
                /*
                 * Set up the categories from the database to a select list item.
                 */
                Categories = (IEnumerable<SelectListItem>)_DB.Categories.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name,
                    Selected = m.Id == 1
                })
            };

            /*
             * If the post is null then we again return not found.
             */
            if (viewModel.Post == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogEditViewModel viewModel)
        {
            /*
             * We make sure that the id passed in the url is the same as the post id in the view.
             */
            if (id != viewModel.Post.Id)
            {
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine(viewModel.Post.Title);

            /*
             * Update the post record on the database
             */
            _DB.Posts.Update(viewModel.Post);

                /*
                 * Then we save the changes to the database
                 */
                await _DB.SaveChangesAsync();
            

            /*
             * Return to the index function
             */
            return RedirectToAction(nameof(Index));
        }

        // GET: Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            /*
             * We check to make sure an id has been passed.
             * Else we want to return the not found.
             */
            if (id == null)
            {
                return NotFound();
            }

            /*
             * Then we setup the blog delete view model.
             */
            BlogDeleteViewModel viewModel = new BlogDeleteViewModel()
            {
                Post = await _DB.Posts.FirstOrDefaultAsync(m => m.Id == id)
            };

            /*
             * Double check the post query didn't return null
             * If it does we want to return not found.
             */
            if (viewModel.Post == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BlogDeleteViewModel viewModel)
        {
            /*
             * We want to retrieve the post from the database.
             * We will utilise the blogs id within the view model.
             */

            try
            {
                /*
                 * Instead of retrieving the record from the database,
                 * We can directly remove it from the database.
                 */
                _DB.Posts.Remove(viewModel.Post);

                /*
                 * We then save the changes to the database
                 */
                await _DB.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            /*
             * Then redirect the user back to the Index function
             */
            return RedirectToAction(nameof(Index));
        }
    }
}
