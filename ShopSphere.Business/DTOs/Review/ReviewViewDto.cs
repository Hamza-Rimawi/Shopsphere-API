using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Review
{
    public class ReviewViewDto
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
        public DateTime ReviewDate { get; set; }
        public string CustomerName { get; set; }

        public string ProductName { get; set; }
    }
}
