using ShopSphere.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class Shipping
    {
        public int ShippingID { get; set; }
        public int OrderID { get; set; }
        public string CarrierName { get; set; }
        public string TrackingNumber { get; set; }
        public int ShippingStatus { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }

    }
    public class ShippingView
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
    public class GetAllShippingRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? ShippingStatus { get; set; }
        public string?  SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
    public class GetShippingCountRequest
    {
        public int? ShippingStatus { get; set; }
        public string? SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
    
    public class UpdateShippingResult
    {
        public int Success { get; set; }
        public string Message { get; set; }
    }
}
