using Shop.Models;

namespace Shop.Areas.Customer.Views.Products
{
    public class GamesVM
    {
        public required IEnumerable<Product> Games { get; set; }
    }
}
