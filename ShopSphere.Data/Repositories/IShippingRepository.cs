using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Models.Entities;

namespace ShopSphere.Data.Repositories
{
    public interface IShippingRepository
    {
        Task<Shipping> GetShippingStatusAsync(int orderId);
        Task<int> GetShippingCountAsync(GetShippingCountRequest getShippingCount);
        Task<IEnumerable<ShippingView>> GetAllShippingAsync(GetAllShippingRequest getAllShipping);

        Task<IEnumerable<Shipping>> GetAllShippingPerCustomer(int Customer);

        
       Task<UpdateShippingResult> UpdateShippingStatusAsync(int OrderID, int NewShippingStatus, int AdminID);

    }
}
