using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Controllers
{
    /*
     * Set the entire controller to utilise our require admin rights policy.
     */
    [Authorize(Policy = "RequireAdminRights")]
    public class AdminController : Controller
    {
        /*
         * Set the database context to the variable.
         */
        private readonly ApplicationDbContext _DB;

        /*
         * Using the constructor to create a new database context
         */
        public AdminController(ApplicationDbContext context)
        {
            _DB = context;
        }

        // GET: /admin
        public async Task<IActionResult> Index()
        {
            /*
             * Setting up a new admin index view model
             */
            AdminIndexViewModel viewModel = new AdminIndexViewModel()
            {
                Users = await _DB.Users.ToListAsync()
            };

            return View(viewModel);
        }
    }
}
