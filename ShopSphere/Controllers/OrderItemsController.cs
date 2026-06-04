using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/OrderItems")]
    [ApiController]
    public class OrderItems : ControllerBase
    {
        private readonly IOrderItemsService _orderItemsService;
        public OrderItems(IOrderItemsService orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }
        [HttpGet("GetOrderItemsByOrderID/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderID(int orderId)
        {
            var orderItems = await _orderItemsService.GetOrderItemsByOrderIdAsync(orderId);
            if (orderItems == null || !orderItems.Any())
            {
                return NotFound();
            }
            return Ok(orderItems);
        }
        

    }
}
