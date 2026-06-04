using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Status { get; set; }

    }
}
