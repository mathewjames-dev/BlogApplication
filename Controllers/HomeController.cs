using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogApplication.Models;
using BlogApplication.Data;
using BlogApplication.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        // Setting the application database context class to the db variable.
        private readonly ApplicationDbContext db;

        // Using the constructor to then assign a new instance of the context to the db variable.
        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }
       

        public async Task<IActionResult> Index()
        {
            List<Post> postList = await db.Posts
                .OrderByDescending(p => p.PostedOn)
                .ToListAsync();

            return View(postList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
