using ShopSphere.Business.DTOs.Order;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IOrderService
    {
        
        Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId);
        Task<int> GetAllOrdersCountAsync(OrderCountRequestDto orderCount);
        Task<IEnumerable<OrderViewDto>> GetAllOrdersAsync(GetAllOrderRequestDto getAllOrder);
        Task<IEnumerable<OrderDto>> GetCustomerHistoryOrdersAsync(int customerId);
        Task<CheckoutDto> CheckoutAsync(CheckoutRequestDto checkoutRequest);
        Task<UpdateOrderResultDto> UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrder);
        Task<UpdateOrderResultDto> CancelOrderByCustomerAsync(CancelOrderRequestDto cancleOrder);

    }
}
