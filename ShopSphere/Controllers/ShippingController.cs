using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Order;
using ShopSphere.Business.DTOs.Shipping;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/Shipping")]
    [ApiController]
    public class Shipping : ControllerBase
    {
        private readonly IShippingService _shippingService;
        public Shipping(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetShippingStatus/{orderId}", Name = "GetShippingStatus")]
        public async Task<IActionResult> GetShippingStatusAsync(int orderId)
        {
            var result = await _shippingService.GetShippingStatusAsync(orderId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetCount", Name = "GetShippingCount")]
        public async Task<IActionResult> GetShippingCount([FromQuery] GetShippingCountRequestDto request)
        {
            var count = await _shippingService.GetShippingCountAsync(request);
            return Ok(count);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetPaged", Name = "GetPagedShipping")]
        public async Task<IActionResult> GetPagedShipping([FromQuery] GetAllShippingRequestDto request)
        {

            if (request.SortOrder != "ASC" && request.SortOrder != "DESC")
                return BadRequest("SortOrder must be either 'ASC' or 'DESC'.");
            
            if (request.SortBy != "ShippingID" && request.SortBy != "OrderID" && request.SortBy != "CustomerName" && request.SortBy != "ShippingStatus"
                && request.SortBy != "EstimatedDeliveryDate")
                return BadRequest("SortBy must be on of That List { 'ShippingID','OrderID', 'CustomerName', 'ShippingStatus' 'EstimatedDeliveryDate'.}");

            var pagedShipping = await _shippingService.GetAllShippingAsync(request);
            return Ok(pagedShipping);
        }

        [HttpGet("GetAllShippingPerCustomer/{CustomerId}", Name = "GetAllShippingPerCustomer")]
        public async Task<IActionResult> GetAllShippingPerCustomerAsync(int CustomerId, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(
     User,
     CustomerId,
     "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();
            var result = await _shippingService.GetAllShippingPerCustomerAsync(CustomerId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateShippingStatus", Name = "UpdateShippingStatusAsync")]
        public async Task<IActionResult> UpdateShippingStatusAsync([FromBody] UpdateShippingDto request)
        {
            try
            {
                var result = await _shippingService.UpdateShippingStatusAsync(request);
                if (result.Success > 0)
                {
                    return Ok(new { Message = result.Message, Success = result.Success });
                }
                return NotFound(new { Message = result.Message, Success = result.Success });
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while updating the shipping status.", Details = ex.Message });
            }
        }
    }
}
