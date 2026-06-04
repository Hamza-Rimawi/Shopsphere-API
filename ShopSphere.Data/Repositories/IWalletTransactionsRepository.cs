using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopSphere.Models.Entities;

namespace ShopSphere.Data.Repositories
{
    public interface IWalletTransactionsRepository
    {
        public Task<IEnumerable<WalletTransactionsView>> GetWalletHistory(int CustomerId);
        public Task<string> AddWalletMoney(AddWalletMoneyRequest addWalletMoneyRequest);
    }
}
