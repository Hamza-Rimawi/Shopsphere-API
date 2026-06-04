using AutoMapper;
using ShopSphere.Business.DTOs.ProductImage;
using ShopSphere.Business.Helpers;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class ProductImageService : IProductImageService
    {
           private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<string> GetProductPrimaryImageAsync(int productId)
        {
            return await _unitOfWork.ProductImages.GetProductPrimaryImageAsync(productId);
        }
        public async Task<IEnumerable<ProductImageDto>> GetProductImagesAsync(int productId)
        {
           var images = await _unitOfWork.ProductImages.GetProductImagesAsync(productId);
            return images.Select(img => _mapper.Map<ProductImageDto>(img));
        }   
        public async Task<int> AddProductImageAsync(AddProductImageDto image)
        {
            var entity = _mapper.Map<ProductImage>(image);
            return await _unitOfWork.ProductImages.AddProductImageAsync(entity);
        }
        public async Task<int> UpdateProductImageAsync(ProductImageDto image)
        {
            var entity = _mapper.Map<ProductImage>(image);
            return await _unitOfWork.ProductImages.UpdateImageOrderAsync(entity);
        }
        public async Task DeleteProductImageAsync(int imageId)
        {
            await _unitOfWork.ProductImages.DeleteProductImageAsync(imageId);
        }


    }
}
