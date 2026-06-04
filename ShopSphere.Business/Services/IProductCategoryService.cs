using ShopSphere.Models.Entities;
using ShopSphere.Business.DTOs.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IProductCategoryService
    {
            Task<ProductCategoryDto> GetCategoryByIdAsync(int categoryId);
            Task<IEnumerable<ProductCategoryDto>> GetAllCategoriesAsync();
            Task<int> AddCategoryAsync(string CategoryName);
            Task<int> UpdateCategoryAsync(ProductCategoryDto category);
            Task DeleteCategoryAsync(int categoryId);

    }
}
