using ShopSphere.Business.Database;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public class ProductCategoryRepository:IProductCategoryRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public ProductCategoryRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<ProductCategory> GetCategoryByIdAsync(int categoryId)
        {
            return  _sqlDataAccess.LoadDataAsync<ProductCategory, dynamic>("SP_GetCategoryByID", new { CategoryID = categoryId })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }
        public Task<IEnumerable<ProductCategory>> GetAllCategoriesAsync()
        {
            return _sqlDataAccess.LoadDataAsync<ProductCategory, dynamic>("SP_GetAllCategories", new { });
        }
        public Task<int> AddCategoryAsync(string CategoryName)
        {
          return _sqlDataAccess.SaveDataAsync("SP_AddNewCategory", new { CategoryName = CategoryName }, "NewCategoryID");
        }
        public Task<int> UpdateCategoryAsync(ProductCategory category)
        {
            return _sqlDataAccess.SaveDataAsync("SP_UpdateCategory", new { CategoryID = category.CategoryID, CategoryName = category.CategoryName });
        }
        public Task DeleteCategoryAsync(int categoryId)
        {
            return _sqlDataAccess.SaveDataAsync("SP_DeleteCategory", new { CategoryID = categoryId });
        }
    }
}
