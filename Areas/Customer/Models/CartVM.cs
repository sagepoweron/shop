using Shop.Models;

namespace Shop.Areas.Customer.Models
{
    public class CartVM
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        //public decimal? TotalPrice { get; set; }
        //public int? TotalCount { get; set; }

        //public double Total => CartItems.Sum(m => m.Amount * m.Product.ListPrice);
        public double Total => CartItems.Sum(m => m.GetTotal());
        public int Amount => CartItems.Sum(item => item.Amount);

    }
}
