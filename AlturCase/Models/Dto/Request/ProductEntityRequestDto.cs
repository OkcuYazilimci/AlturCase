using System.ComponentModel.DataAnnotations;

namespace AlturCase.Models.Dto.Request
{
    public class ProductEntityRequestDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive integer.")]
        public int Stock { get; set; }

        public string? Description { get; set; }
    }
}
