using AutoMapper;
using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.Shipping;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class ShippingService : IShippingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShippingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ShippingDto> GetShippingStatusAsync(int orderId)
        {
            var shipping = await _unitOfWork.Shipping.GetShippingStatusAsync(orderId);
            return _mapper.Map<ShippingDto>(shipping);
        }
        public async Task<int> GetShippingCountAsync(GetShippingCountRequestDto getShippingCount)
        {
            return await _unitOfWork.Shipping.GetShippingCountAsync(_mapper.Map<GetShippingCountRequest>(getShippingCount));
        }
        public async Task<IEnumerable<ShippingViewDto>> GetAllShippingAsync(GetAllShippingRequestDto getAllShipping)
        {
            var result = await _unitOfWork.Shipping.GetAllShippingAsync(_mapper.Map<GetAllShippingRequest>(getAllShipping));
            return _mapper.Map<IEnumerable<ShippingViewDto>>(result);
        }

        public async Task<IEnumerable<ShippingDto>> GetAllShippingPerCustomerAsync(int CustomerId)
        {
            var shipping = await _unitOfWork.Shipping.GetAllShippingPerCustomer(CustomerId);
            return _mapper.Map<IEnumerable<ShippingDto>>(shipping);
        }

        public async Task<UpdateShippingResultDto> UpdateShippingStatusAsync(UpdateShippingDto updateShippingDto)
        {
            var reuslt = await _unitOfWork.Shipping.UpdateShippingStatusAsync(updateShippingDto.OrderID, updateShippingDto.newShippingStatus, updateShippingDto.adminID);
            return _mapper.Map<UpdateShippingResultDto>(reuslt);
            
        }

    }
}
