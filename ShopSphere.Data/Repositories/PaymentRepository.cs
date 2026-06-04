using ShopSphere.Business.Database;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public PaymentRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<IEnumerable<Payment>> GetCustomerPaymentsAsync(int customerId)
        {
            return _sqlDataAccess.LoadDataAsync<Payment, dynamic>("SP_GetCustomerPayments", new { CustomerID = customerId });
        }
    }
}
