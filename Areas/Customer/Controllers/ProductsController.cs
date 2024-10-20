using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Areas.Customer.Models;
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
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Product.Include(p => p.Category);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(string category)
        {
            //IQueryable<Product> query = from product in _context.Product select product;
            //if (!string.IsNullOrEmpty(category))
            //{
            //    //RAW SQL
            //    var parameter = new SqliteParameter("comparison", $"%{category}%");
            //    query = _context.Product.FromSqlRaw("SELECT * FROM Product WHERE category LIKE @comparison", parameter);
            //}

            var query = _context.Product
                .OrderBy(s => s.Category.Name)
                .Include(p => p.Category);

            if (!string.IsNullOrEmpty(category))
            {
                query = _context.Product
                    .Where(s => s.Category.Name.ToLower().Contains(category.ToLower()))
                    .OrderBy(s => s.Category.Name)
                    .Include(s => s.Category);
            }

            return View(await query.ToListAsync());
        }


        // GET: Customer/Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
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

        //public async Task<IActionResult> List(string category)
        //{
        //    List<Product> products;

        //    if (string.IsNullOrEmpty(category))
        //    {
        //        products = await _context.Product.Include(d => d.Category).ToListAsync();
        //    }
        //    else
        //    {
        //        products = await _context.Product
        //            .Where(item => item.Category.Name.ToLower().Contains(category.ToLower()))
        //            //.OrderBy(d => d.Name)
        //            .Include(d => d.Category)
        //            .ToListAsync();
        //    }

        //    ProductsVM vm = new()
        //    {
        //        //Category = category.Pascalize(),
        //        Products = products
        //    };


        //    return View(vm);
        //}
    }
}
