using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Payment;
using ShopSphere.Business.Services;

namespace ShopSphere.Controllers
{
    [Authorize]
    [Route("api/ShopSphere/Payment")]
    [ApiController]
    public class Payment : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public Payment(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet("GetCustomerPayments/{customerId}")]
        public async Task<IActionResult> GetCustomerPaymentMethods(int customerId, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(
     User,
     customerId,
     "UserOwnerOrAdmin");

                if (!authResult.Succeeded)
                    return Forbid();
                var paymentMethods = await _paymentService.GetCustomerPaymentsAsync(customerId);
                if (paymentMethods != null)
                {
                    return Ok(paymentMethods);
                }
                else
                {
                    return NotFound("No payment  found for the specified customer.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, "An error occurred while retrieving payment .");
            }
        }
    }
}