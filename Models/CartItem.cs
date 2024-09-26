using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        //public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        [Range(0, 100, ErrorMessage = "Limit of 100")]
        public int Amount { get; set; }



        //[ForeignKey(nameof(UserId))]
        //[ValidateNever]
        //public IdentityUser User { get; set; }

        [ForeignKey(nameof(ProductId))]
        [ValidateNever]
        public Product Product { get; set; }


        public double GetTotal()
        {
            return Product.ListPrice * Amount;
        }
    }
}
