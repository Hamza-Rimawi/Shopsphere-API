using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Review;
using ShopSphere.Business.Services;
using ShopSphere.Models.Entities;


namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/Review")]
    [ApiController]
    public class Review : ControllerBase
    {
       
        private readonly IReviewService _reviewService;
        public Review(IReviewService reviewService, IAuthorizationService authorizationService)
        {
            _reviewService = reviewService;
           
        }
        [AllowAnonymous]
        [HttpGet("AverageRating/{productId}", Name = "GetProductAverageRatingAsync")]
        public async Task<IActionResult> GetProductAverageRatingAsync(int productId)
        {
            var averageRating = await _reviewService.GetProductAverageRatingAsync(productId);
            return Ok(averageRating);
        }
        [AllowAnonymous]
        [HttpGet("ProductReviews/{productId}", Name = "GetProductReviewsAsync")]
        public async Task<IActionResult> GetProductReviewsAsync(int productId)
        {
            var reviews = await _reviewService.GetProductReviewsAsync(productId);
            if (reviews == null || !reviews.Any())
                return NotFound();
            return Ok(reviews);
        }
        [Authorize]
        [HttpGet("CustomerReviews/{CustomerId}", Name = "GetCustomerReviewsAsync")]
        public async Task<IActionResult> GetCustomerReviewsAsync(int CustomerId, [FromServices] IAuthorizationService authorizationService)
        {
            var reviews = await _reviewService.GetCustomerReviewsAsync(CustomerId);
            if (reviews == null || !reviews.Any())
                return NotFound();
             
            var authResult = await authorizationService.AuthorizeAsync(
        User,
        CustomerId,
        "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid(); // 403
            return Ok(reviews);
        }
        [Authorize]
        [HttpGet("CheckReview", Name = "GetCheckReviewAsync")]
        public async Task<IActionResult> GetCheckReviewAsync([FromQuery] CheckReviewDto check, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(
      User,
      check.CustomerID,
      "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid(); // 403
            var reviews = await _reviewService.CheckReviewAsync(check);
            if (reviews == 1)
                return Ok("There is  Review for that product from the that Customer.");
            else
                return NotFound("There is no Review for that product from the that Customer.");
        }
        [Authorize]
        [HttpPost(Name = "AddReviewAsync")]
        public async Task<IActionResult> AddReviewAsync([FromBody] NewReviewDto reviewDto)
        {
            try
            {

                var createdReviewId = await _reviewService.AddReviewAsync(reviewDto);
                return CreatedAtAction("Created",createdReviewId);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPut("{reviewId}", Name = "UpdateReviewAsync")]
        public async Task<IActionResult> UpdateReviewAsync(int reviewId, [FromBody] UpdateReviewDto reviewDto)
        {
            try
            {

                await _reviewService.UpdateReviewAsync(reviewDto, reviewId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpDelete("{reviewId}", Name = "DeleteReviewAsync")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(reviewId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }



    }
}

