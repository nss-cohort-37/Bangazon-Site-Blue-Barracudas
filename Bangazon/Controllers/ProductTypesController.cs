using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.ProductTypeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
   [Authorize]
   public class ProductTypesController : Controller
   {
       private readonly ApplicationDbContext _context;
       private readonly UserManager<ApplicationUser> _userManager;
       public ProductTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
       {
          _context = context;
          _userManager = userManager;
       }


       // GET: ProductTypes
       public async Task<ActionResult> Index()
        {
            var productTypes = await Types();
            return View(productTypes);
        }

        // GET: ProductTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
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

        // GET: ProductTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductTypes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductTypes/Delete/5
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

        public async Task<IActionResult> Types()
        {
            var model = new ProductTypesViewModel();

            model.Types = await _context
                .ProductType
                .Select(pt => new TypeWithProducts()
                {
                    TypeId = pt.ProductTypeId,
                    TypeName = pt.Label,
                    ProductCount = pt.Products.Count(),
                    Products = pt.Products.OrderByDescending(p => p.DateCreated).Take(3).ToList()
                }).ToListAsync();

            return View(model);
        }
    }
}