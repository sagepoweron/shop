using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customer/Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Customer/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        private bool ProductExists(Guid id)
        {
            return _context.Product.Any(e => e.Id == id);
        }



        public async Task<IActionResult> List(string? id)
        {
            List<Product> products;

            if (string.IsNullOrEmpty(id))
            {
                products = await _context.Product.ToListAsync();
            }
            else
            {
                products = await _context.Product.Where(item => item.Brand == id).ToListAsync();
            }


            return View(products);
        }
    }
}
