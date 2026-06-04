using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.DTOs.ProductCategory;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/ProductCategory")]
    [ApiController]
    public class ProductCategory : ControllerBase
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategory(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }
        [AllowAnonymous]
        [HttpGet("GetAll", Name = "GetAllCategories")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Add", Name = "AddCategory")]
        public async Task<IActionResult> Add([FromBody] string CategoryName)
        {
            try
            {
                var newId = await _categoryService.AddCategoryAsync(CategoryName);
                return CreatedAtRoute("GetCategoryById", new { id = newId }, newId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update", Name = "UpdateCategory")]
        public async Task<IActionResult> Update([FromBody] ProductCategoryDto categoryDto)
        {
            try
            {
                var result = await _categoryService.UpdateCategoryAsync(categoryDto);
                if (result == 0)
                    return NotFound();
                else
                    return Ok($"{result} rows of Category  updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}", Name = "DeleteCategory")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                return Ok($"Category with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
