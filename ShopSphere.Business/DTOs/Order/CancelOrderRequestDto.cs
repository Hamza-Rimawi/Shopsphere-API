using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Order
{
    public class CancelOrderRequestDto
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
    }
}
