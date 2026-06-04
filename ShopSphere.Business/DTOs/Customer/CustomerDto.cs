using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.Customer
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public bool Role { get; set; }
        [Required]
        public DateTime CreatedDate {get; set;}
        [Required]
        public bool IsActive { get; set; }

    }

}
