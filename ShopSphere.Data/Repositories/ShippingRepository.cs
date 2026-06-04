using ShopSphere.Business.Database;
using ShopSphere.Models.Entities;
using ShopSphere.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public class ShippingRepository : IShippingRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public ShippingRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
            
        public Task<Shipping> GetShippingStatusAsync(int orderId)
        {   
          return _sqlDataAccess.LoadDataAsync<Shipping, dynamic>("SP_GetShippingStatus", new { OrderId = orderId })
                .ContinueWith(task => task.Result.FirstOrDefault());        
        }
        
        public Task<IEnumerable<Shipping>> GetAllShippingPerCustomer(int CustomerId)
        {
            return _sqlDataAccess.LoadDataAsync<Shipping, dynamic>("SP_GetAllShippingPerCustomer", new { CustomerID = CustomerId });
        }
        public Task<int> GetShippingCountAsync(GetShippingCountRequest getShippingCount)
        {
            return _sqlDataAccess.ExecuteScalarAsync<int, dynamic>("SP_GetShippingCount", getShippingCount);
        }
        public Task<IEnumerable<ShippingView>> GetAllShippingAsync(GetAllShippingRequest getAllShipping)
        {
            return _sqlDataAccess.LoadDataAsync<ShippingView, dynamic>("SP_GetAllShippingsByPage", getAllShipping);
        }

        public Task<UpdateShippingResult> UpdateShippingStatusAsync(int orderID, int newShippingStatus, int adminID)

        {
            var parameters = new
            {
                OrderID = orderID,
                NewShippingStatus = newShippingStatus,
                AdminID = adminID,
            };
            return _sqlDataAccess.LoadDataAsync<UpdateShippingResult,dynamic>("SP_UpdateShippingInfo", parameters)
                .ContinueWith(task=>task.Result.FirstOrDefault());
        }



    }
}
