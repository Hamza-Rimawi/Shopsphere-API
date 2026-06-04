using ShopSphere.Business.DTOs.Review;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IReviewService
    {
            Task<IEnumerable<ReviewViewDto>> GetProductReviewsAsync(int ProductId);
            Task<IEnumerable<ReviewViewDto>> GetCustomerReviewsAsync(int CustomerId);
        
            Task<decimal>GetProductAverageRatingAsync(int ProductId);
            Task<int> AddReviewAsync(NewReviewDto reviewDto);
            Task<int> UpdateReviewAsync(UpdateReviewDto reviewDto, int reviewId);
            Task<int> DeleteReviewAsync(int reviewId);
        Task<int> CheckReviewAsync(CheckReviewDto check);
    }
}
