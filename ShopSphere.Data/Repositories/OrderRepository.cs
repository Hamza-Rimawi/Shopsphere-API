using ShopSphere.Business.Database;
using ShopSphere.Data.Repositories;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopSphere.Business.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public OrderRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        
        public Task<IEnumerable<Order>> GetCustomerOrdersAsync(int customerId)
        {
            return _sqlDataAccess.LoadDataAsync<Order, dynamic>("SP_GetCustomerOrders", new { CustomerID = customerId });
        }
        public Task<IEnumerable<Order>> GetCustomerHistoryOrdersAsync(int customerId)
        {
            return _sqlDataAccess.LoadDataAsync<Order, dynamic>("SP_GetCustomerHistoryOrders", new { CustomerID = customerId });
        }
        public Task<int> GetAllOrdersCountAsync(OrderCountRequest orderCount)
        {
            return _sqlDataAccess.ExecuteScalarAsync<int, dynamic>("SP_OrdersCount", orderCount);
        }
        public Task<IEnumerable<OrderView>> GetAllOrdersAsync(GetAllOrderRequest getAllOrder)
        {
            return _sqlDataAccess.LoadDataAsync<OrderView, dynamic>("SP_GetAllOrders", getAllOrder);

        }

        public Task<IEnumerable<Checkout>> CheckoutAsync(int customerId, string paymentMethod, List<CartItemParameter> cartItems)
        {
            var cartXml = BuildCartXml(cartItems);
            var parameters = new {
                CustomerID = customerId, 
                PaymentMethod = paymentMethod,
                CartItemsXML = cartXml 
            };
            return _sqlDataAccess.LoadDataAsync<Checkout, dynamic > ("SP_Checkout", parameters);
        }

        public Task<UpdateOrderResult> UpdateOrderStatusAsync (int orderId, int newStatus)
        {
            return _sqlDataAccess.LoadDataAsync<UpdateOrderResult,dynamic>("SP_UpdateOrderStatus", new { OrderID = orderId, NewStatus = newStatus })
                .ContinueWith(task => task.Result.FirstOrDefault()); 
        }
        public Task<UpdateOrderResult> CancelOrderByCustomerAsync(int orderId, int customerid) {
            return _sqlDataAccess.LoadDataAsync<UpdateOrderResult, dynamic>("SP_CancelOrder", new { OrderID = orderId, CustomerID = customerid })
                  .ContinueWith(task => task.Result.FirstOrDefault());
        }

        private string BuildCartXml(List<CartItemParameter> cartItems)
        {
            var xml = new XDocument(
                new XElement("Cart",
                    cartItems.Select(item =>
                        new XElement("Item",
                            new XAttribute("ProductID", item.ProductID),
                            new XAttribute("Quantity", item.Quantity),
                            new XAttribute("Price", item.Price)
                        )
                    )
                )
            );

            return xml.ToString();
        }
    }
}
