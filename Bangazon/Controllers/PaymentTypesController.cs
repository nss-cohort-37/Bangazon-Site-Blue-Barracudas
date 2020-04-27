using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    [Authorize]
    public class PaymentTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: PaymentTypes
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var paymentTypes = await _context.PaymentType
             .Where(ti => ti.UserId == user.Id)
             .ToListAsync();

            return View(paymentTypes);


        }

        // GET: PaymentTypes/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }
        // POST: PaymentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType paymentType)
       {
            try
            {

                var user = await GetCurrentUserAsync();
                paymentType.UserId = user.Id;

                _context.PaymentType.Add(paymentType);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch
            {

                return View();
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            var paymentType = await _context.PaymentType.FirstOrDefaultAsync(pt => pt.PaymentTypeId == id);

            var user = await GetCurrentUserAsync();

            if (paymentType.UserId != user.Id)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, PaymentType paymentType)
        {
            try
            {
                paymentType.PaymentTypeId = id;
                _context.PaymentType.Remove(paymentType);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}

