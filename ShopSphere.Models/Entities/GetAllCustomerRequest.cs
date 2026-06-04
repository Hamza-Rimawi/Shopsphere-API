using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class GetAllCustomerRequest
    {
        public int PageNumber { get; set; }
        public int RowsLength { get; set; }
        public string? SearchTerm { get; set; }
        public string? SearchColumn { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
      
    }
}
