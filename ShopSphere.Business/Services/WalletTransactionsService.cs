using AutoMapper;
using ShopSphere.Business.DTOs.WalletTransactions;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class WalletTransactionsService : IWalletTransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WalletTransactionsService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WalletTransactionsViewDto>> GetWalletTransactionsAsync(int CustmoerId)
        {
            var reuslt = await _unitOfWork.WalletTransactions.GetWalletHistory(CustmoerId);
            return _mapper.Map<IEnumerable<WalletTransactionsViewDto>>(reuslt);
        }
        public async Task<string> AddWalletMoneyAsync(AddWalletMoneyRequestDto addWalletMoneyRequest)
        {
            var request = _mapper.Map<AddWalletMoneyRequest>(addWalletMoneyRequest);
            return await _unitOfWork.WalletTransactions.AddWalletMoney(request);
        }

    }
}
