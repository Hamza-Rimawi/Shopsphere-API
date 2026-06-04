using System;
using System.Collections.Generic;
using ShopSphere.Models.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface  IRefreshTokenRepository
    {
        public Task<int> AddRefreshTokenAsync(AddRefreshToken refreshToken);
        public Task<IEnumerable<RefreshToken>> GetRefreshTokenAsync(int CustomerID);
        public Task<int> RevokeRefreshTokenAsync(int RefreshTokenID);

    }
}
