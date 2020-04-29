using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.OrderViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            
            var orderProducts = await _context.OrderProduct
                                    .Include(op => op.Product)
                                    .Where(op => op.Order.UserId == user.Id && op.Order.PaymentTypeId == null).ToListAsync();
            var orderId = await _context.Order.FirstOrDefaultAsync(o => o.UserId == user.Id && o.PaymentTypeId == null);

            var viewModel = new ShoppingCartViewModel();
            if (orderId != null && orderProducts != null)
            {
                viewModel = new ShoppingCartViewModel()
                {
                    OrderId = orderId.OrderId,
                    Products = orderProducts
                };


            }
            else
            {
                viewModel = new ShoppingCartViewModel()
                {
                    OrderId = 0,
                    Products = null
                };
            }
            return View(viewModel);

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
        public async Task<ActionResult> Edit(int id)
        {
            var paymentOptions = await _context.PaymentType
              .Select(pt => new SelectListItem() { Text = pt.Description, Value = pt.PaymentTypeId.ToString() })
              .ToListAsync();

            var viewModel = new OrderPaymentFormViewModel();

            viewModel.PaymentTypeOptions = paymentOptions;
            viewModel.OrderId = id; 

            return View(viewModel);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OrderPaymentFormViewModel orderPayment)
        {
            try
            {
                var user = await GetCurrentUserAsync();

                var order = new Order()
                {
                    OrderId = id,
                    PaymentTypeId = orderPayment.PaymentTypeId,
                    DateCompleted = DateTime.Now, 
                    UserId = user.Id
                };

                _context.Order.Update(order);
                await _context.SaveChangesAsync(); 


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
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


        // delete for the entire order and all it's corresponding products 
        
        
        public async Task<ActionResult> CancelOrder(int id)
        {
            var item = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == id);



            return View(item);
        }

        // POST: Orders/CancelOrder/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelOrder(int id, OrderProduct orderProduct, Order order)
        {
            try
            {
                var orderToDelete = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == id);

                _context.Order.Remove(orderToDelete);
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