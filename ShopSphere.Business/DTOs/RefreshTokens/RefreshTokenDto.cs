using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.DTOs.RefreshTokens
{
    public class RefreshTokenDto
    {
        public int RefreshTokenID { get; set; }
        public int CustomerID { get; set; }
        public string TokenHash { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenRevokedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
