using System;
using System.Collections.Generic;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<IEnumerable<OrderItems>> GetOrderItemsAsync(int orderId);
    }
}
