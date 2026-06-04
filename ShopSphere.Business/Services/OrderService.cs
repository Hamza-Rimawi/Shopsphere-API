using AutoMapper;
using ShopSphere.Business.DTOs.Order;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
         public async Task<IEnumerable<OrderDto>> GetCustomerOrdersAsync(int customerId)
        {
            var orders = await _unitOfWork.Orders.GetCustomerOrdersAsync(customerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
        public async Task<IEnumerable<OrderDto>> GetCustomerHistoryOrdersAsync(int customerId)
        {
            var orders = await _unitOfWork.Orders.GetCustomerHistoryOrdersAsync(customerId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
        public async Task<int> GetAllOrdersCountAsync(OrderCountRequestDto orderCount)
        {
            var request = _mapper.Map<OrderCountRequest>(orderCount);
            return await _unitOfWork.Orders.GetAllOrdersCountAsync(request);
        }
        public async Task<IEnumerable<OrderViewDto>> GetAllOrdersAsync(GetAllOrderRequestDto getAllOrder)
        {
            var request = _mapper.Map<GetAllOrderRequest>(getAllOrder);
            var result = await _unitOfWork.Orders.GetAllOrdersAsync(request);
            return  _mapper.Map<IEnumerable<OrderViewDto>>(result);
        }
        public async Task<CheckoutDto> CheckoutAsync(CheckoutRequestDto checkoutRequest)
        {
            if (checkoutRequest.CartItems == null || !checkoutRequest.CartItems.Any())
            {
                throw new Exception("Cart is empty");
            }
            var cartItems = _mapper.Map<List<CartItemParameter>>(checkoutRequest.CartItems);

            var checkoutResult = await _unitOfWork.Orders.CheckoutAsync(checkoutRequest.CustomerID,checkoutRequest.PaymentMethod, cartItems);
            return _mapper.Map<CheckoutDto>(checkoutResult.FirstOrDefault());
        }

        public async Task<UpdateOrderResultDto> UpdateOrderStatusAsync(UpdateOrderStatusDto updateOrder)
        {
            var result = await _unitOfWork.Orders.UpdateOrderStatusAsync(updateOrder.OrderID, updateOrder.NewStatus);
            return _mapper.Map<UpdateOrderResultDto>(result);
        }
        public async Task<UpdateOrderResultDto> CancelOrderByCustomerAsync(CancelOrderRequestDto cancleOrder)
        {
            var result = await _unitOfWork.Orders.CancelOrderByCustomerAsync(cancleOrder.OrderID, cancleOrder.CustomerID);
            return _mapper.Map<UpdateOrderResultDto>(result);
        }
    }
}
