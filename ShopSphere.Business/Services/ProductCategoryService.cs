using System;
using System.Collections.Generic;
using ShopSphere.Business.Helpers;
using ShopSphere.Business.DTOs.ProductCategory;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ShopSphere.Business.Services
{
    public class ProductCategoryService: IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductCategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _unitOfWork.ProductCategories.GetCategoryByIdAsync(categoryId);
            return _mapper.Map<ProductCategoryDto>(category);
        }
        public async Task<IEnumerable<ProductCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.ProductCategories.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<ProductCategoryDto>>(categories);
        }
        public async Task<int> AddCategoryAsync(string CategoryName)
        {
            return await _unitOfWork.ProductCategories.AddCategoryAsync(CategoryName);
        }
        public async Task<int> UpdateCategoryAsync(ProductCategoryDto category)
        {
            var entity = _mapper.Map<ProductCategory>(category);
            return await _unitOfWork.ProductCategories.UpdateCategoryAsync(entity);
        }
        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _unitOfWork.ProductCategories.DeleteCategoryAsync(categoryId);
        }
        
    }
}
