using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShopSphere.Models.Entities;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IProductRepository
    {
       
        Task<Product> GetProductByIdAsync(int productId);
        Task<int> GetProductCountAsync(int? categoryId = null, string? searchTerm = null);
        Task<IEnumerable<ProductView>> GetProductsbyPageAsync(ProductPageRequest pageRequest);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<Product> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<int> AddProductAsync(Product product, string ImageURL);
        Task<int> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);

    }
}
