using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class ProductPageRequest
    {
       public int PageNumber { get; set; }
        public int RowsLength { get; set; }
        public int? CategoryID { get; set; }
        public string? SearchTerm { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }

    }    
}
