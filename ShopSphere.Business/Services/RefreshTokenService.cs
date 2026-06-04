using AutoMapper;
using System;
using System.Collections.Generic;
using ShopSphere.Models.Entities;
using ShopSphere.Business.DTOs.RefreshTokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RefreshTokenService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<int> AddRefreshTokenAsync(AddRefreshTokenDto refreshToken)
        {
            var entity = _mapper.Map<AddRefreshToken>(refreshToken);
            return _unitOfWork.RefreshTokenRepository.AddRefreshTokenAsync(entity);
        }
        public async Task<IEnumerable<RefreshTokenDto>> GetRefreshTokenAsync(int CustomerID)
        {
            var entities = await _unitOfWork.RefreshTokenRepository.GetRefreshTokenAsync(CustomerID);
            return _mapper.Map<IEnumerable<RefreshTokenDto>>(entities);
        }
        public Task<int> RevokeRefreshTokenAsync(int RefreshTokenID)
        {
            return _unitOfWork.RefreshTokenRepository.RevokeRefreshTokenAsync(RefreshTokenID);
        }
    }
}
