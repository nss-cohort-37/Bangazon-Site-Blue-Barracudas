using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.ProfileViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Profile
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var userInfo = await _context.ApplicationUsers
                .Where(o => o.Id == user.Id)
                .Include(o => o.Orders)
                .Include(p => p.Products)
                .Include(pt => pt.PaymentTypes)
                .ToListAsync();

            var userId = await _context.ApplicationUsers.FirstOrDefaultAsync(o => o.Id == user.Id);

            var viewModel = new ProfileDetailsViewModel()
            {
                User = user, 
                UserId = userId.Id,
                Products = userId.Products.ToList(),
                Orders = userId.Orders.ToList(),
                PaymentTypes = userId.PaymentTypes.ToList(),

        };



            return View(viewModel);

        }
        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var viewModel = new ProfileFormViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StreetAddress = user.StreetAddress
            };
            return View(viewModel);
        }

        // POST: Profiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProfileFormViewModel profile)
        {
            try
            {
                var profileData = await GetCurrentUserAsync();

                profileData.FirstName = profile.FirstName;
                profileData.LastName = profile.LastName;
                profileData.StreetAddress = profile.StreetAddress;


          

                _context.ApplicationUsers.Update(profileData);
              
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}