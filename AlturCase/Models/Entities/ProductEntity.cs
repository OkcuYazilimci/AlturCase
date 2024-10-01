using System.ComponentModel.DataAnnotations;

namespace AlturCase.Models.Entities
{
    public class ProductEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string? UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive integer")]
        public int Stock { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
