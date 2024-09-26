using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Other
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = "Default";

        //[Display(Name = "Image Path")]
        [Required]
        public required string Path { get; set; }
    }
}
