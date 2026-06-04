using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }

    }

    public class OrderView
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public int ShippingStatus { get; set; }
        public string TrackingNumber { get; set; }
     
    }
    public class GetAllOrderRequest
    {
        public int PageNumber { get;set;}
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public int? StatusFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string SortBy { get; set; }

        public string SortOrder { get; set; }

    }
    public class OrderCountRequest
    {
        public string? SearchTerm { get; set; }
        public int? StatusFilter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
    public class UpdateOrderResult
    {
        public int Success { get; set; }
        public string Message { get; set; }
    }
}
