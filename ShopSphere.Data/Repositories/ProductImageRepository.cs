using ShopSphere.Business.Database;
using ShopSphere.Data;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ShopSphere.Data.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public ProductImageRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId)
        {
            return _sqlDataAccess.LoadDataAsync<ProductImage, dynamic>("SP_GetProductImages", new { ProductID = productId });
        }
     
        public Task<int> AddProductImageAsync(ProductImage image)
        {
            var parameters = new
            {
                image.ProductId,
                image.ImageUrl,
                image.ImageOrder
            };
            return _sqlDataAccess.SaveDataAsync("SP_AddProductImage", parameters, "NewImageID");
        }
        public Task<int> UpdateImageOrderAsync(ProductImage image)
        {
            var parameters = new
            {
                ImageID = image.ImageId,
               ImageURL=image.ImageUrl,
                NewOrder =image.ImageOrder
            };
            return _sqlDataAccess.SaveDataAsync("SP_UpdateImageOrder", parameters);
        }
        public Task DeleteProductImageAsync(int imageId)
        {
            return _sqlDataAccess.SaveDataAsync("SP_DeleteProductImage", new { ImageID = imageId });
        }
        public Task<string> GetProductPrimaryImageAsync(int productId)
        {
            return _sqlDataAccess.ExecuteScalarAsync<string, dynamic>("SP_GetProductPrimaryImage", new { ProductID = productId });
        }
    
    }

}