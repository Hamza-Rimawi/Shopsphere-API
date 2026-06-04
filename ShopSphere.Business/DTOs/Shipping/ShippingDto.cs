using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Shipping
{
    public class ShippingDto
    {
        public int ShippingID { get; set; }
        public int OrderID { get; set; }
        public string CarrierName { get; set; }
        public string TrackingNumber { get; set; }
        public int ShippingStatus { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime ActualDeliveryDate { get; set; }
    }
}
