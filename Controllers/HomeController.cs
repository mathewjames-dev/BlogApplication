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
        /*
         * Setting the database context to the DB variable.
         */
        private readonly ApplicationDbContext _DB;

        /*
         * Using the constructor to then assign that variable an instance of the database context.
         */
        public HomeController(ApplicationDbContext context)
        {
            _DB = context;
        }
       
        public async Task<IActionResult> Index()
        {
            /*
             * Setting up a new instance of the index view model,
             * setting the posts value to the query result.
             */
            IndexViewModel viewModel = new IndexViewModel()
            {
                Posts = await _DB.Posts
                .OrderByDescending(p => p.PostedOn)
                .ToListAsync(),
                IsSignedIn = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
