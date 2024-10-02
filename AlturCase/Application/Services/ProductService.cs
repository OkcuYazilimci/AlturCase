using AlturCase.Core.Interfaces;
using AlturCase.Core.Dto.Request.Product;
using AlturCase.Core.Entities;
using AlturCase.Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AlturCase.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductEntity> CreateProduct(ProductCreateDto productCreateDto, Guid userId)
        {
            var productEntity = new ProductEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                Stock = productCreateDto.Stock,
                Description = productCreateDto.Description,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now
            };

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return productEntity;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ProductEntity> GetProductById(Guid productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<List<ProductEntity>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductEntity> UpdateProduct(Guid productId, ProductUpdateDto productUpdateDto)
        {
            var productEntity = await _context.Products.FindAsync(productId);

            if (productEntity == null)
            {
                throw new ArgumentException("Product not found!");
            }

            _mapper.Map(productUpdateDto, productEntity);
            await _context.SaveChangesAsync();

            return productEntity;
        }
    }
}
