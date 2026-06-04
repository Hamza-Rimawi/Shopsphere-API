using AutoMapper;
using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.Product;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public  ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<int> GetProductCountAsync(ProductCountRequestDto request)
        {
            return await _unitOfWork.Products.GetProductCountAsync(request.CategoryId, request.SearchTerm);
        }
        public async Task<IEnumerable<ProductViewDto>> GetProductsByPageAsync(ProductPageRequestDto parameters)
        {
            var products = await _unitOfWork.Products.GetProductsbyPageAsync(_mapper.Map<ProductPageRequest>(parameters));
            return _mapper.Map<IEnumerable<ProductViewDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetProductsByCategoryAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<ProductDto> GetProductByNameAsync(string productName)
        {
            var product = await _unitOfWork.Products.GetProductByNameAsync(productName);
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<int> AddProductAsync(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            return await _unitOfWork.Products.AddProductAsync(product,addProductDto.ImageURL);
        }
        public async Task<int> UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            return await _unitOfWork.Products.UpdateProductAsync(product);
        }
        public async Task DeleteProductAsync(int productId)
        {
            await _unitOfWork.Products.DeleteProductAsync(productId);
        }

    }
}
