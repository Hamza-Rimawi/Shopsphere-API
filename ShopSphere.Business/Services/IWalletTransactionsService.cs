using ShopSphere.Business.DTOs.WalletTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface IWalletTransactionsService
    {
        public Task<IEnumerable<WalletTransactionsViewDto>> GetWalletTransactionsAsync(int CustmoerId);
        public Task<string> AddWalletMoneyAsync(AddWalletMoneyRequestDto addWalletMoneyRequest);
    }
}
