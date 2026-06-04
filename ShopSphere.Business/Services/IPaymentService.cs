using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.Payment;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetCustomerPaymentsAsync(int customerId);
    }
}
