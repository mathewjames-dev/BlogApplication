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
        public IActionResult Create(BlogCreateViewModel viewModel)
        {
            System.Diagnostics.Debug.WriteLine(viewModel.Post.Title);
            System.Diagnostics.Debug.WriteLine(viewModel.Post.CategoryId);

            return Redirect("/admin");
        }

        // GET: Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _DB.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UrlSlug,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _DB.Update(category);
                    await _DB.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
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
            var post = await _DB.Posts.FindAsync(viewModel.Post.Id);

            /*
             * Once we retrieve the post we can then proceed to remove that record,
             * from the post table within the database.
             */
            _DB.Posts.Remove(post);

            /*
             * We then save the changes to the database
             */
            await _DB.SaveChangesAsync();

            /*
             * Then redirect the user back to the Index function
             */
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _DB.Categories.Any(e => e.Id == id);
        }
    }
}
