using AutoMapper;
using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.OrderItems;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderItemsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderItemsDto>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orderItems = await _unitOfWork.Orderitems.GetOrderItemsAsync(orderId);
            return _mapper.Map<IEnumerable<OrderItemsDto>>(orderItems);

        }
       
    }
    
}
