using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.Product;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Models.Entities;

namespace ShopSphere.Business.Services
{
     public interface IProductService
    {
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<int> GetProductCountAsync(ProductCountRequestDto request);
        Task<IEnumerable<ProductViewDto>> GetProductsByPageAsync(ProductPageRequestDto parameters);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<ProductDto> GetProductByNameAsync(string productName);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<int> AddProductAsync(AddProductDto addProductDto);
        Task<int> UpdateProductAsync(ProductDto product);
        Task DeleteProductAsync(int productId);
    }
}
