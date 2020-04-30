using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }


    // GET: Products
    public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var products = await _context.Product
                .Where(p => p.UserId == user.Id)
                .Include(p => p.ProductType)
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var item = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == id);

            var viewModel = new ProductDetailViewModel();

            viewModel.Id = id; 
            viewModel.Title = item.Title;
            viewModel.Price = item.Price;
            viewModel.Description = item.Description;
            viewModel.Quantity = item.Quantity;
            viewModel.ImagePath = item.ImagePath;
            

            return View(viewModel);
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var ProductTypeOptions = await _context.ProductType
              .Select(pt => new SelectListItem() { Text = pt.Label , Value = pt.ProductTypeId.ToString() })
              .ToListAsync();

            var viewModel = new ProductFormViewModel();

            viewModel.ProductTypeOptions = ProductTypeOptions; 


            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ProductId,DateCreated,Description,Title,Price,Quantity,UserId,City,ImagePath,Active,ProductTypeId,File")]ProductFormViewModel productViewItem)
        {
            try
            {
                var user = await GetCurrentUserAsync(); 

                var product = new Product
                {
                    DateCreated = productViewItem.DateCreated, 
                    Description = productViewItem.Description, 
                    Title = productViewItem.Title, 
                    Price = productViewItem.Price, 
                    Quantity = productViewItem.Quantity, 
                    UserId = user.Id, 
                    City = productViewItem.City, 
                    Active = productViewItem.Active, 
                    ProductTypeId = productViewItem.ProductTypeId, 
                    localDelivery = productViewItem.localDelivery,
                    
                };
                if (productViewItem.File != null && productViewItem.File.Length > 0)
                {
                    //creates the file name
                    var fileName = Guid.NewGuid().ToString() + Path.GetFileName(productViewItem.File.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    product.ImagePath = fileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productViewItem.File.CopyToAsync(stream);
                    }

                }

                _context.Product.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _context.Product.Include(p => p.ProductType).FirstOrDefaultAsync(p => p.ProductId == id);
            return View(item);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product product)
        {
            try
            {
                product.ProductId = id;
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}