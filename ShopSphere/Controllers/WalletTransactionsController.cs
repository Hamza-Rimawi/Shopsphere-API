using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.WalletTransactions;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/WalletTransactions")]
    [ApiController]
    public class WalletTransactions : ControllerBase
    {
        private readonly IWalletTransactionsService _walletTransactionsService;
        public WalletTransactions(IWalletTransactionsService walletTransactionsService)
        {
            _walletTransactionsService = walletTransactionsService;
        }

        [HttpGet("GetWalletTransactions/{CustomerId}", Name = "GetWalletTransactions")]
        public async Task<IActionResult> GetWalletTransactions(int CustomerId, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(
     User,
     CustomerId,
     "UserOwnerOrAdmin");

            if (!authResult.Succeeded)
                return Forbid();
            var WalletTransactions = await _walletTransactionsService.GetWalletTransactionsAsync(CustomerId);
            return Ok(WalletTransactions);
        }

        [HttpPost("AddWalletMoney", Name = "AddWalletMoneyAsync")]
        public async Task<IActionResult> AddWalletMoney([FromBody] AddWalletMoneyRequestDto addWalletMoneyRequest, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(
     User,
     addWalletMoneyRequest.CustomerID,
     "UserOwnerOrAdmin");

                if (!authResult.Succeeded)
                    return Forbid();
                var resulte = await _walletTransactionsService.AddWalletMoneyAsync(addWalletMoneyRequest);
                return Ok(resulte);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest("Failed to add money to the wallet");
                }
            }
        }
    }
}
