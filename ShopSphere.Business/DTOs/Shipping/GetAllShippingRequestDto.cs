using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Shipping
{
    public class GetAllShippingRequestDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? ShippingStatus { get; set; }
        public string? SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
