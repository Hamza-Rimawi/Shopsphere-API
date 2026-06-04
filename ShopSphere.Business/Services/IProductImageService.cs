using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.ProductImage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IProductImageService
    {
        Task<string> GetProductPrimaryImageAsync(int productId);
        Task<IEnumerable<ProductImageDto>> GetProductImagesAsync(int productId);
        Task<int> AddProductImageAsync(AddProductImageDto image);
        Task<int> UpdateProductImageAsync(ProductImageDto image);
        Task DeleteProductImageAsync(int imageId);
    }
}
