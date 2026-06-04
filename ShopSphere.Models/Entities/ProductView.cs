using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class ProductView
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int quantityInStock { get; set; }
        public string categoryName { get; set; }
        public decimal averageRating { get; set; }

    }
}