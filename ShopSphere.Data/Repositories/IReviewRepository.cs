using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<ReviewView>> GetProductReviewsAsync(int ProductId);
        Task<IEnumerable<ReviewView>> GetCustomerReviewsAsync(int CustomerId);
        Task<decimal>GetProductAverageRatingAsync(int ProductId);
        Task<int> AddReviewAsync(Review review);
        Task<int> UpdateReviewAsync(Review review);
        Task<int> DeleteReviewAsync(int reviewId);
        Task<int> CheckReviewAsync(int ProductId,int CustomerId);
    }
}
