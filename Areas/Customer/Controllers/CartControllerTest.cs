using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Areas.Customer.Models;
using Shop.Data;
using Shop.Models;

namespace Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
	//[Authorize(Roles = Helpers.Customer_Role)]
	public class CartControllerTest : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartControllerTest(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer/CartItems
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.CartItem.Include(c => c.Product);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index()
        {
            List<CartItem> cart_items = await _context.CartItem.Include(c => c.Product).ToListAsync();

            CartVM vm = new()
            {
                CartItems = cart_items
                //TotalPrice = (decimal)cart_items.Sum(item => item.Product.ListPrice * item.Amount),
                //TotalCount = cart_items.Sum(item => item.Amount)
            };
            return View(vm);
        }



        // GET: Customer/CartItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CartItem cart_item)
        {
            cart_item.Id = Guid.NewGuid();

            //ClaimsIdentity claims_identity = (ClaimsIdentity)User.Identity;
            //string user_id = claims_identity.FindFirst(ClaimTypes.NameIdentifier).Value;

            CartItem? cart_item_db = await _context.CartItem.FirstOrDefaultAsync(m => m.ProductId == cart_item.ProductId/* && m.UserId == user_id*/);
            if (cart_item_db != null)
            {
                cart_item_db.Amount++;
            }
            else
            {
                await _context.CartItem.AddAsync(cart_item);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), nameof(CartController));
        }













        // GET: Customer/CartItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", cartItem.ProductId);
            return View(cartItem);
        }

        // POST: Customer/CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductId,Amount")] CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.CartItem.Any(e => e.Id == cartItem.Id))
                    //if (!CartItemExists(cartItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", cartItem.ProductId);
            return View(cartItem);
        }

        // GET: Customer/CartItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItem
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: Customer/CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cartItem = await _context.CartItem.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItem.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(Guid id)
        {
            return _context.CartItem.Any(e => e.Id == id);
        }


        




    }
}
