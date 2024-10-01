using AlturCase.Models.Dto.Request;
using AlturCase.Models.Entities;

namespace AlturCase.Services.Abstract
{
    public interface IProductService
    {
        Task<ProductEntity> CreateProduct(ProductEntityRequestDto productEntityRequestDto, string userId);

        public Task<ProductEntity> GetProductById(Guid id);
    }
}