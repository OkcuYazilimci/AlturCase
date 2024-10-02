using AlturCase.Application.Utils;
using AlturCase.Core.Interfaces;
using AlturCase.Core.Dto.Request.Product;
using AlturCase.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlturCase.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = (Guid)HttpContext.Items["UserId"];

            var productEntity = await _productService.CreateProduct(productCreateDto, userId);

            return CreatedAtAction(nameof(GetProductById), new { id = productEntity.Id }, productEntity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product could not found!");
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.GetProducts();
            if (product == null)
            {
                return NotFound("Products could not found!");
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        [CtxUser(typeof(ProductEntity))]
        public async Task<IActionResult> UpdateProduct(Guid productId, [FromBody] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateProduct = await _productService.UpdateProduct(productId, productUpdateDto);

            if (updateProduct == null)
            {
                return NotFound("Product not found or could not be updated.");
            }

            return Ok(updateProduct);
        }

        [HttpDelete("{id}")]
        [CtxUser(typeof(ProductEntity))]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var deletedProduct = await _productService.DeleteProduct(id);

                if (deletedProduct == null)
                {
                    return NotFound("Product not found or could not be deleted.");
                }

                return Ok("Product deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }
    }
}
