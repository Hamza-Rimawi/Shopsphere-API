using System;
using System.Collections.Generic;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId);
        Task<int> GetAllOrdersCountAsync(OrderCountRequest orderCount);
        Task<IEnumerable<OrderView>> GetAllOrdersAsync(GetAllOrderRequest getAllOrder);

        Task<IEnumerable<Order>> GetCustomerHistoryOrdersAsync(int customerId);
        Task<IEnumerable<Checkout>> CheckoutAsync(int customerId,string paymentMethod,List<CartItemParameter> cartItems);
        Task<UpdateOrderResult> UpdateOrderStatusAsync(int orderId, int newStatus);
        Task<UpdateOrderResult> CancelOrderByCustomerAsync(int orderId, int customerid); 
    }
}
