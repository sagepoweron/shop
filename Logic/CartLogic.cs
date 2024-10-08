using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Logic
{
    public class CartLogic
    {
        private readonly ApplicationDbContext _context;

        public CartLogic(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Guid? id)
        {
            if (id == null)
            {
                return;
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return;
            }

            CartItem? cart_item = await _context.CartItem.FirstOrDefaultAsync(m => m.ProductId == id);
            if (cart_item != null)
            {
                cart_item.Amount++;
            }
            else
            {
                cart_item = new()
                {
                    Product = product,
                    Amount = 1
                };
                await _context.CartItem.AddAsync(cart_item);
            }

            await _context.SaveChangesAsync();

            return;
        }

    }
}
