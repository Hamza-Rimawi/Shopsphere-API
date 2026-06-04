using ShopSphere.Business.DTOs.Shipping;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IShippingService
    {
        Task<ShippingDto> GetShippingStatusAsync(int orderId);
        Task<int> GetShippingCountAsync(GetShippingCountRequestDto getShippingCount);
        Task<IEnumerable<ShippingViewDto>> GetAllShippingAsync(GetAllShippingRequestDto getAllShipping);
        Task<IEnumerable< ShippingDto>> GetAllShippingPerCustomerAsync(int CustomerId);
        Task<UpdateShippingResultDto> UpdateShippingStatusAsync(UpdateShippingDto updateShippingDto);
    }
}
