using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.ProductImage
{
    public class AddProductImageDto
    {

        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public int ImageOrder { get; set; }
    }
}
