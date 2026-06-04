using ShopSphere.Business.Database;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public class ReviewRepository : IReviewRepository   
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public ReviewRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public  Task<int> AddReviewAsync(Review review)
        {
           var Parameters = new
            {
                review.ProductId,
                review.CustomerId,
                review.ReviewText,
                review.Rating,
                review.ReviewDate
            };
           return _sqlDataAccess.SaveDataAsync("SP_AddReview", Parameters, "NewReviewID");
        }
        public  Task<IEnumerable<ReviewView>> GetProductReviewsAsync(int productId)
        {
            return  _sqlDataAccess.LoadDataAsync<ReviewView, dynamic>("SP_GetProductReviews", new { ProductID = productId });
        }
        public Task<IEnumerable<ReviewView>> GetCustomerReviewsAsync(int CustomerId)
        {
            return _sqlDataAccess.LoadDataAsync<ReviewView, dynamic>("SP_GetCustomerReviews", new { CustomerID = CustomerId });
        }

        public Task<int> DeleteReviewAsync(int reviewId)
        {
            return  _sqlDataAccess.SaveDataAsync("SP_DeleteReview", new { ReviewID = reviewId });
        }
        public  Task<decimal> GetProductAverageRatingAsync(int productId)
        {
           return  _sqlDataAccess.ExecuteScalarAsync<decimal, dynamic>("SP_GetProductAverageRating", new { ProductID = productId }, "AverageRating");
            
        }
        public  Task<int> UpdateReviewAsync(Review review)
        {
            var Parameters = new
            {
                review.ReviewId,
                review.ReviewText,
                review.Rating,
            };
            return  _sqlDataAccess.SaveDataAsync("SP_UpdateReview", Parameters);
        }
        public Task<int> CheckReviewAsync(int ProductId, int CustomerId)
        {
            var Parameters = new
            {
                ProductID=ProductId,
                CustomerID= CustomerId
            };
            return _sqlDataAccess.ExecuteScalarAsync<int,dynamic>("SP_CheckReview", Parameters, "Result");
        }

    }
}
