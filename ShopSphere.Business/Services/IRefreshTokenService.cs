using System;
using System.Collections.Generic;
using ShopSphere.Business.DTOs.RefreshTokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IRefreshTokenService
    {
        public Task<int> AddRefreshTokenAsync(AddRefreshTokenDto refreshToken);
        public Task<IEnumerable<RefreshTokenDto>> GetRefreshTokenAsync(int CustomerID);
        public Task<int> RevokeRefreshTokenAsync(int RefreshTokenID);
    }
}
