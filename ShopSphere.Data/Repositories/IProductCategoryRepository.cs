using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IProductCategoryRepository
    {

        Task<ProductCategory> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync();
        Task<int> AddCategoryAsync(string CategoryName);
        Task<int> UpdateCategoryAsync(ProductCategory category);
        Task DeleteCategoryAsync(int categoryId);


    }
}
