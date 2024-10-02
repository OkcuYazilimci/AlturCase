namespace AlturCase.Core.Dto.Request.Product
{
    public class ProductUpdateDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? Stock { get; set; }
    }
}
