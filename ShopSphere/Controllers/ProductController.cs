using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.DTOs.Product;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/Product")]
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly IProductService _productservice;
        public Product(IProductService productservice)
        {

            _productservice = productservice;

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll", Name = "GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productservice.GetAllProductsAsync();
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("GetById/{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productservice.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [AllowAnonymous]
        [HttpGet("GetCount", Name = "GetProductCount")]
        public async Task<IActionResult> GetProductCount([FromQuery] ProductCountRequestDto request)
        {
            var count = await _productservice.GetProductCountAsync(request);
            return Ok(count);
        }
        [AllowAnonymous]
        [HttpGet("GetPaged", Name = "GetPagedProducts")]
        public async Task<IActionResult> GetPagedProducts([FromQuery] ProductPageRequestDto request)
        {
            
            if (request.SortOrder != "asc" && request.SortOrder != "desc")
                return BadRequest("SortOrder must be either 'asc' or 'desc'.");
            if(request.SortBy != "ProductID" && request.SortBy != "Price" && request.SortBy != "ProductName")
                return BadRequest("SortBy must be either 'ProductID', 'Price', or 'ProductName'.");

            var pagedProducts = await _productservice.GetProductsByPageAsync(request);
            return Ok(pagedProducts);
        }
        [Authorize]
        [HttpGet("GetByName/{name}", Name = "GetProductByName")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _productservice.GetProductByNameAsync(name);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [Authorize]
        [HttpGet("GetByCategory/{categoryId}", Name = "GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productservice.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Add", Name = "AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDto product)
        {
            try
            {
                var newId = await _productservice.AddProductAsync(product);
                return CreatedAtRoute("GetProductById", new { id = newId }, newId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update", Name = "UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product)
        {
            try
            {
                var result = await _productservice.UpdateProductAsync(product);
                if (result == 0)
                    return NotFound();
                else
                    return Ok($"{result} rows of Product  updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}", Name = "DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productservice.DeleteProductAsync(id);
                return Ok($"Product with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
