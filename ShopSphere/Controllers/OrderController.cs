using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Order;
using ShopSphere.Business.DTOs.Product;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/Order")]
    [ApiController]
    public class Order : ControllerBase
    {
        private readonly IOrderService _orderService;
        public Order(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet("CustomerOrders/{customerId}", Name = "GetCustomerOrders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(
     User,
     customerId,
     "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();
            var orders = await _orderService.GetCustomerOrdersAsync(customerId);
            return Ok(orders);
        }
        [HttpGet("CustomerHistoryOrders/{customerId}", Name = "GetCustomerHistoryOrders")]
        public async Task<IActionResult> GetCustomerHistoryOrders(int customerId, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(
     User,
     customerId,
     "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();
            var orders = await _orderService.GetCustomerHistoryOrdersAsync(customerId);
            return Ok(orders);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetCount", Name = "GetOrdersCount")]
        public async Task<IActionResult> GetOrdersCount([FromQuery] OrderCountRequestDto request)
        {
            var count = await _orderService.GetAllOrdersCountAsync(request);
            return Ok(count);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetPaged", Name = "GetPagedOrders")]
        public async Task<IActionResult> GetPagedOrders([FromQuery] GetAllOrderRequestDto request)
        {
            
            if (request.SortOrder != "asc" && request.SortOrder != "desc")
                return BadRequest("SortOrder must be either 'asc' or 'desc'.");
            if (request.SortBy != "OrderID" && request.SortBy != "CustomerName" && request.SortBy != "TotalAmount" 
                && request.SortBy != "OrderDate" && request.SortBy != "Status")
                return BadRequest("SortBy must be on of That List { 'OrderID', 'CustomerName', 'TotalAmount' 'OrderDate', 'Status'.}");

            var pagedOrders = await _orderService.GetAllOrdersAsync(request);
            return Ok(pagedOrders);
        }
        [HttpPost("Checkout", Name = "Checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(
     User,
     request.CustomerID,
     "UserOwnerOrAdmin");

                if (!authResult.Succeeded)
                    return Forbid();
                // Validate request
                if (request == null)
                {
                    return BadRequest(new { Message = "Invalid request data" });
                }

                if (request.CartItems == null || !request.CartItems.Any())
                {
                    return BadRequest(new { Message = "Cart is empty" });
                }

                // Process checkout
                var result = await _orderService.CheckoutAsync(request);

                // Check if checkout was successful
                if (result == null || result.OrderID <= 0)
                {
                    return BadRequest(new { Message = result?.Message ?? "Checkout failed" });
                }

                // Return success response
                return Ok(new
                {
                    Success = true,
                    OrderId = result.OrderID,
                    Message = result.Message,
                    TotalAmount = result.TotalAmount
                });
            }
            catch (Exception ex)
            {
                // Log the exception (you should implement logging)
                // _logger.LogError(ex, "Error during checkout");

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "An error occurred during checkout",
                    Error = ex.Message
                });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateOrderStatus", Name = "UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusDto request)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(request);
                if (result.Success > 0)
                {
                    return Ok(new { Message = result.Message, Success = result.Success });
                }
                else
                {
                    return NotFound(new { Message = result.Message, Success = result.Success });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while updating order status.", Details = ex.Message });
            }
        }
        [HttpPut("CancelOrderByCustomer", Name = "CancelOrderByCustomer")]
        public async Task<IActionResult> CancelOrderByCustomer([FromBody] CancelOrderRequestDto request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(
     User,
     request.CustomerID,
     "UserOwnerOrAdmin");

                if (!authResult.Succeeded)
                    return Forbid();
                var result = await _orderService.CancelOrderByCustomerAsync(request);
                if (result.Success > 0)
                {
                    return Ok(new { Message = result.Message , Success=result.Success });
                }
                else
                {
                    return NotFound(new { Message = result.Message, Success = result.Success });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while Canceling order status.", Details = ex.Message });
            }
        }
    }
}
