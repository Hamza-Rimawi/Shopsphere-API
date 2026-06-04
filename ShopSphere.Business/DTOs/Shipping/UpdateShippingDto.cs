using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Shipping
{
    public class UpdateShippingDto
    {
        public int OrderID { get; set; }
        public int newShippingStatus { get; set; }
        public int adminID { get; set; }
        
    }
}
