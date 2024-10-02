using AlturCase.Core.Dto.Request.Product;
using AlturCase.Core.Entities;

namespace AlturCase.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductEntity> CreateProduct(ProductCreateDto productEntityRequestDto, Guid userId);

        public Task<ProductEntity> GetProductById(Guid productId);

        public Task<List<ProductEntity>> GetProducts();

        public Task<ProductEntity> UpdateProduct(Guid productId, ProductUpdateDto productUpdateDto);
        public Task<bool> DeleteProduct(Guid productId);
    }
}