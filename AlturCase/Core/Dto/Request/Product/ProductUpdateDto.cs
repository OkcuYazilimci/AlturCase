using System.ComponentModel.DataAnnotations;

namespace AlturCase.Core.Dto.Request.Product
{
    public class ProductUpdateDto
    {
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be at least 1")]
        public int Stock { get; set; }
        public string? Description { get; set; }
    }
}
