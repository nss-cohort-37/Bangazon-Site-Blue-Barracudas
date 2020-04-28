using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Models;
using Bangazon.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index(string searchBar)
        {
            if (searchBar != null)
            {
                var products = await _context.Product
                      .Where(p => p.Title.Contains(searchBar) && p.Active == true || p.City.Contains(searchBar) && p.Active == true)
                      .Include(p => p.ProductType)
                      //.Include(p => p.ImagePath)
                       .ToListAsync();
                return View(products);
            }
            else
            {
                var products = await _context.Product
                    .Where(p => p.Active == true)
                    .Include(p => p.ProductType)
                     //.Include(p => p.ImagePath)
                   .ToListAsync();
                return View(products);
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
