using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IProductImageRepository
    {
        Task<int> AddProductImageAsync(ProductImage productImage);
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);
        Task<string> GetProductPrimaryImageAsync(int productId);
        Task DeleteProductImageAsync(int productId);
        Task <int> UpdateImageOrderAsync(ProductImage productImage);

    }
}
