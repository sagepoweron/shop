using Shop.Models;

namespace Shop.Areas.Customer.Models
{
    public class GamesVM
    {
        public required IEnumerable<Product> Games { get; set; }
    }
}
