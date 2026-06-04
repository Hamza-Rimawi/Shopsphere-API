using AutoMapper;
using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.Payment;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class PaymentService : IPaymentService   
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PaymentDto>> GetCustomerPaymentsAsync(int customerId)
        {
            var paymentMethods = await _unitOfWork.Payments.GetCustomerPaymentsAsync(customerId);
            return _mapper.Map<IEnumerable<PaymentDto>>(paymentMethods);
        }
    }
}
