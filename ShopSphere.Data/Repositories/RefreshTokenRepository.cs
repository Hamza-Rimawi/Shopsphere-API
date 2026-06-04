using ShopSphere.Business.Database;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public RefreshTokenRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<int> AddRefreshTokenAsync(AddRefreshToken refreshToken)
        {
            return _sqlDataAccess.SaveDataAsync("SP_AddRefreshTokens", refreshToken);
        }
        public Task<IEnumerable<RefreshToken>> GetRefreshTokenAsync(int CustomerID)
        {
            return _sqlDataAccess.LoadDataAsync<RefreshToken, dynamic>("SP_GetActiveRefreshTokens", new { CustomerID });
        }
        public Task<int> RevokeRefreshTokenAsync(int RefreshTokenID)
        {
            return _sqlDataAccess.SaveDataAsync("SP_RevokeRefreshTokens", new { RefreshTokenID });
        }
    }
}
