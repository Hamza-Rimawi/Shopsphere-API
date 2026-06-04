using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class Checkout
    {
        public int OrderID { get; set; }
        public string Message { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
