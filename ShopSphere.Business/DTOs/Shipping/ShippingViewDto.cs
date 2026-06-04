using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Shipping
{
    public class ShippingViewDto
    {
        public int ShippingID { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CarrierName { get; set; }

        public string TrackingNumber { get; set; }
        public int ShippingStatus { get; set; }
        public int OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public int DaysSinceOrder { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
    }
}
