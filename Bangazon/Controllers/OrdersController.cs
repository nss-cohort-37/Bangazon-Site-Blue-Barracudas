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
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {

            _context = context;
            _userManager = userManager;
        }

        public async  Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var shoppingCartItems = await _context.OrderProduct
                .Include(op => op.Order)
                .Where(op => op.Order.UserId == user.Id && op.Order.PaymentTypeId == null)
                .Include(op => op.Product).ToListAsync();

                //.Include(op => op.Order.OrderProducts ).ToListAsync(); 

            
            return View(shoppingCartItems);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderProduct orderProduct, int id)
        {
            try
            {
                // Add button clicked from details page, need to search if a cart is there or not
                var user = await GetCurrentUserAsync();


                var shoppingCartExists = _context.Order.FirstOrDefault(o => o.UserId == user.Id && o.PaymentTypeId == null); 

                if(shoppingCartExists == null)
                {
                    //make a cart which is an order with payment type null etc 

                    var shoppingCart = new Order
                    {
                        UserId = user.Id, 
                        PaymentTypeId = null 
                    };

                    _context.Order.Add(shoppingCart);
                    await _context.SaveChangesAsync();

                    var NewOrderProduct = new OrderProduct
                    {
                        OrderId = shoppingCart.OrderId,
                        ProductId = id
                    };

                    _context.OrderProduct.Add(NewOrderProduct);
                    await _context.SaveChangesAsync();

                }
                else
                {


                    var Order =  _context.Order.FirstOrDefault(o => o.UserId == user.Id && o.PaymentTypeId == null); 

                    var AddingOrderProduct = new OrderProduct
                    {
                        OrderId = Order.OrderId,
                        ProductId = id
                    }; 

                    _context.OrderProduct.Add(AddingOrderProduct);
                    await _context.SaveChangesAsync(); 

                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _context.OrderProduct.Include(op => op.Product).FirstOrDefaultAsync(op => op.OrderProductId == id);

            return View(item);
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Delete(int id, OrderProduct orderProduct)
        {
            try
            {
                orderProduct.OrderProductId = id;
                _context.OrderProduct.Remove(orderProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}