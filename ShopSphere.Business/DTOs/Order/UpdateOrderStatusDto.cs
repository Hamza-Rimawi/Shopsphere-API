using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Order
{
    public class UpdateOrderStatusDto
    {
        public int OrderID { get; set; }
        public int NewStatus { get; set; }
    }
}
