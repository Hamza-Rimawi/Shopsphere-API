using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Product
{
    public class ProductCountRequestDto
    {
        public int? CategoryId { get; set; }
        public string? SearchTerm { get; set; }
    }
}
