using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.WalletTransactions
{
    public class AddWalletMoneyRequestDto
    {
        public int CustomerID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

    }
}
