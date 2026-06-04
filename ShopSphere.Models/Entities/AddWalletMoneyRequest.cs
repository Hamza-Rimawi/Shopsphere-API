using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class AddWalletMoneyRequest
    {
        public int CustomerID {  get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }

    }
}
