using ShopSphere.Business.DTOs.OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItemsDto>> GetOrderItemsByOrderIdAsync(int orderId);

    }
    
}
