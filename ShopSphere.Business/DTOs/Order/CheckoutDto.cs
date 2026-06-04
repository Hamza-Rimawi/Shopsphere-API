using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Order
{
    public class CheckoutDto
    {
        public int OrderID { get; set; }
        public string Message { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
