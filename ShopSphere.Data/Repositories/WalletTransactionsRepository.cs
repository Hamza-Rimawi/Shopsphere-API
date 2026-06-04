using ShopSphere.Business.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Models.Entities;

namespace ShopSphere.Data.Repositories
{
    public class WalletTransactionsRepository: IWalletTransactionsRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;
        public WalletTransactionsRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<IEnumerable<WalletTransactionsView>> GetWalletHistory(int customerID)
        {
            return _sqlDataAccess.LoadDataAsync<WalletTransactionsView, dynamic>("SP_GetWalletHistory", new{ CustomerID = customerID});
        }
        public Task<string> AddWalletMoney(AddWalletMoneyRequest addWalletMoneyRequest)
        {
            return _sqlDataAccess.ExecuteScalarAsync<string, dynamic>("SP_AddWalletMoney", addWalletMoneyRequest);
               
        }
    }
}
