using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.DTOs.ProductImage;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/ProductImages")]
    [ApiController]
    public class ProductImages : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImages(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [AllowAnonymous]
        [HttpGet("{productId}", Name = "GetProductImagesAsync")]
        public async Task<IActionResult> GetProductImagesAsync(int productId)
        {
            var images = await _productImageService.GetProductImagesAsync(productId);
            if (images == null || !images.Any())
                return NotFound();
            return Ok(images);
        }
        [AllowAnonymous]
        [HttpGet("GetProductPrimaryImageAsync/{productId}", Name = "GetProductPrimaryImageAsync")]
        public async Task<IActionResult> GetProductPrimaryImageAsync(int productId)
        {
            var image = await _productImageService.GetProductPrimaryImageAsync(productId);
            if (image == null)
                return NotFound();
            return Ok(image);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "AddProductImageAsync")]
        public async Task<IActionResult> AddProductImageAsync([FromBody] AddProductImageDto imageDto)
        {
            try { 
                var createdImage = await _productImageService.AddProductImageAsync(imageDto);
                return Ok(new { ProductimageId = createdImage, Message = "Product added successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut(Name = "UpdateProductImageAsync")]
        public async Task<IActionResult> UpdateProductImageAsync([FromBody] ProductImageDto imageDto)
        {
            try
            {
                var result =  await _productImageService.UpdateProductImageAsync(imageDto);
                if (result == 0)
                    return NotFound();
                else
                    return Ok($"{result} rows of image  updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{imageId}", Name = "DeleteProductImageAsync")]
        public async Task<IActionResult> DeleteProductImageAsync(int imageId)
        {
            try
            {
                await _productImageService.DeleteProductImageAsync(imageId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
