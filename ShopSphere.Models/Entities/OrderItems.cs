using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Models.Entities
{
    public class OrderItems
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalItemsPrice { get; set; }
        public int? ReviewId { get; set; }
        public string? ReviewText { get; set; }
        public decimal? ReviewRating { get; set; }
        public DateTime? ReviewDate { get; set; }

    }
}
