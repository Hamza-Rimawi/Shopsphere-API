using System;
using System.Collections.Generic;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetCustomerPaymentsAsync(int customerId);

    }
}
