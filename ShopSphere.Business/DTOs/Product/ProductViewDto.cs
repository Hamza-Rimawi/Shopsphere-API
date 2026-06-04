using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Product
{
    public class ProductViewDto
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
