using System;
using System.Collections.Generic;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Business.Database;

namespace ShopSphere.Data.Repositories
{
    public class OrderItemsRepository: IOrderItemsRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public OrderItemsRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<IEnumerable<OrderItems>> GetOrderItemsAsync(int orderId)
        {
            return _sqlDataAccess.LoadDataAsync<OrderItems, dynamic>("SP_GetOrderItems", new { OrderID = orderId });
        }
        
    }
    
}
