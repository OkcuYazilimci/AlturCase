using AlturCase.Data;
using AlturCase.Models.Dto.Request;
using AlturCase.Models.Entities;
using AlturCase.Services.Abstract;

namespace AlturCase.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductEntity> CreateProduct(ProductEntityRequestDto productEntityRequestDto, string userId)
        {
            var productEntity = new ProductEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId, // This should come from the user context
                Name = productEntityRequestDto.Name,
                Price = productEntityRequestDto.Price,
                Stock = productEntityRequestDto.Stock,
                Description = productEntityRequestDto.Description,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now
            };

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return productEntity;
        }

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
