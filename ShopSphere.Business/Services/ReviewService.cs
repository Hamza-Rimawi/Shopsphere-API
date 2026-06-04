using AutoMapper;
using ShopSphere.Business.DTOs.Review;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> AddReviewAsync(NewReviewDto reviewDto)
        {
            var review = _mapper.Map<Review>(reviewDto);
            return await _unitOfWork.Reviews.AddReviewAsync(review);
        }
        public async Task<IEnumerable<ReviewViewDto>> GetProductReviewsAsync(int productId)
        {
            var reviews = await _unitOfWork.Reviews.GetProductReviewsAsync(productId);
            return reviews.Select(r => _mapper.Map<ReviewViewDto>(r));
        }
        public async Task<IEnumerable<ReviewViewDto>> GetCustomerReviewsAsync(int CustomerId)
        {
            var reviews = await _unitOfWork.Reviews.GetCustomerReviewsAsync(CustomerId);
            return reviews.Select(r => _mapper.Map<ReviewViewDto>(r));
        }
        public async Task<int> DeleteReviewAsync(int reviewId)
        {
            return await _unitOfWork.Reviews.DeleteReviewAsync(reviewId);
        }
        public async Task<decimal> GetProductAverageRatingAsync(int productId)
        {
            return await _unitOfWork.Reviews.GetProductAverageRatingAsync(productId);
        }
        public async Task<int> UpdateReviewAsync(UpdateReviewDto reviewDto, int reviewId)
        {
            var review = _mapper.Map<Review>(reviewDto);
            review.ReviewId = reviewId;
            return await _unitOfWork.Reviews.UpdateReviewAsync(review);
        }
        public async Task<int> CheckReviewAsync(CheckReviewDto check)
        {
            return await _unitOfWork.Reviews.CheckReviewAsync(check.ProductID, check.CustomerID);
        }
    }
}
