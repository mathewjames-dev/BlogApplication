using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers
{
    public class AdminController : Controller
    {
        // Setting the application database context class to the db variable.
        private readonly ApplicationDbContext db;

        // Using the constructor to then assign a new instance of the context to the db variable.
        public AdminController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: /admin
        public async Task<IActionResult> Index()
        {
            // Using the database context we setup to get the users all saved to a list.
            List<IdentityUser> usersList = await db.Users.ToListAsync();
            return View(usersList);
        }
    }
}
