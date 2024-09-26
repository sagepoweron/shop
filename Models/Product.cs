using Shop.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        //[Display(Name = "Category ID")]
        //public Guid CategoryId { get; set; }
        //[ForeignKey(nameof(CategoryId))]
        //public Category? Category { get; set; }

        [Required]
        public string Name { get; set; } = "Default";
        public string? Brand { get; set; }
        public string? Description { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(0, 1000)]
        public double ListPrice { get; set; }

        [Display(Name = "Sale Price")]
        [Range(0, 1000)]
        public double SalePrice { get; set; }


        //public bool IsOnSale { get; set; }

        //public double GetPrice()
        //{
        //    if (IsOnSale)
        //    {
        //        return SalePrice;
        //    }
        //    else
        //    {
        //        return ListPrice;
        //    }
        //}

        //[Display(Name = "Test Price")]
        //public Currency Price { get; set; }

        //[Display(Name = "Image")]
        //public string? ImageUrl { get; set; }
    }
}
