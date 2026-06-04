using ShopSphere.Business.Database;
using ShopSphere.Data.Repositories;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public ProductRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<Product> GetProductByIdAsync(int productId)
        {
            return _sqlDataAccess.LoadDataAsync<Product, dynamic>("SP_GetProductByID", new { ProductID = productId })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }
        public Task<int> GetProductCountAsync(int? categoryId = null, string? searchTerm = null)
        {
            var parameters = new
            {
                CategoryID = categoryId,
                SearchTerm = searchTerm
            };
            return _sqlDataAccess.ExecuteScalarAsync<int, dynamic>("SP_GetProductsCount", parameters);
                
        }
        public Task<IEnumerable<ProductView>> GetProductsbyPageAsync(ProductPageRequest pageRequest)
        {
            var parameters = new
            {
                PageNumber = pageRequest.PageNumber,
                RowsLength = pageRequest.RowsLength,
                CategoryID = pageRequest.CategoryID,
                SearchTerm = pageRequest.SearchTerm,
                SortBy = pageRequest.SortBy,
                SortOrder = pageRequest.SortOrder
            };
            return _sqlDataAccess.LoadDataAsync<ProductView, dynamic>("SP_GetProductsByPage", parameters);
        }

        public Task<Product> GetProductByNameAsync(string productName)
        {
            return _sqlDataAccess.LoadDataAsync<Product, dynamic>("SP_GetProductByName", new { ProductName = productName })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }
        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return _sqlDataAccess.LoadDataAsync<Product, dynamic>("SP_GetProductsByCategory", new { CategoryID = categoryId });
        }
        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _sqlDataAccess.LoadDataAsync<Product, dynamic>("SP_GetAllProducts", new { });
        }
        public Task<int> AddProductAsync(Product product, string ImageURL)
        {
            var parameters = new
            {
                product.ProductName,
                product.Description,
                product.Price,
                product.QuantityInStock,
                product.CategoryID,
                product.CreatedBy,
                ImageURL
            };
            return _sqlDataAccess.SaveDataAsync("SP_AddNewProduct", parameters, "NewProductID");
        }
        public Task<int> UpdateProductAsync(Product product)
        {
            var parameters = new
            {
                product.ProductID,
                product.ProductName,
                product.Description,
                product.Price,
                product.QuantityInStock,
                product.CategoryID
            };
            return _sqlDataAccess.SaveDataAsync("SP_UpdateProduct", parameters);
        }
        public Task DeleteProductAsync(int productId)
        {
            return _sqlDataAccess.SaveDataAsync("SP_DeleteProduct", new { ProductID = productId });
        }
    }
}
