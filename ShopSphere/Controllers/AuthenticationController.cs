using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopSphere.Business.DTOs.Auth;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.DTOs.RefreshTokens;
using ShopSphere.Business.Services;
using ShopSphere.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ShopSphere.Controllers
{
    [Route("api/ShopSphere/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly SymmetricSecurityKey _key;

       
        public AuthenticationController(ICustomerService customerService, IRefreshTokenService refreshTokenService, SymmetricSecurityKey key)
        {
            _customerService = customerService;
            _refreshTokenService = refreshTokenService;
            _key = key;
        }
        [HttpPost("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var customer = await _customerService.GetByUserNameAsync(loginDto.Username);
                if (customer == null)
                    return Unauthorized("Invalid credentials");
                else
                {

                    // Step 2: Verify the provided password against the stored hash.
                    // BCrypt handles hashing and salt internally.
                    bool isValidPassword =
                        BCrypt.Net.BCrypt.Verify(loginDto.Password, customer.Password);


                    // If the password does not match the stored hash,
                    // return 401 Unauthorized.
                    if (!isValidPassword)
                        return Unauthorized("Invalid credentials");


                    // Step 3: Create claims that represent the authenticated user's identity.
                    // These claims will be embedded inside the JWT.
                    var claims = new[]
                    {
                // Unique identifier for the student
                new Claim(ClaimTypes.NameIdentifier, customer.CustomerID.ToString()),


                // Student email address
                new Claim(ClaimTypes.Name, customer.Username),


                // Role (Student or Admin) used later for authorization
                new Claim(ClaimTypes.Role, customer.Role ? "Admin" : "User")
                    };


                    // Step 4: Create the symmetric security key used to sign the JWT.
                    // This key must match the key used in JWT validation middleware.
                    



                    // Step 5: Define the signing credentials.
                    // This specifies the algorithm used to sign the token.
                    var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);


                    // Step 6: Create the JWT token.
                    // The token includes issuer, audience, claims, expiration, and signature.
                    var token = new JwtSecurityToken(
                        issuer: "ShopSphereApi",
                        audience: "ShopSphereApiUsers",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );
                    var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                    // Create refresh token (random)
                    var refreshToken = GenerateRefreshToken();
                    AddRefreshTokenDto addRefreshTokenDto = new AddRefreshTokenDto
                    {
                        CustomerID = customer.CustomerID,
                        TokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken),
                        RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7),
                        CreatedAt = DateTime.UtcNow
                    };

                    await _refreshTokenService.AddRefreshTokenAsync(addRefreshTokenDto);

                    customer.Password = "";
                    return Ok(new 
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        User = customer
                    });


                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var customer = await _customerService.GetByUserNameAsync(request.UserName);
            IEnumerable<RefreshTokenDto> RefreshToken = await _refreshTokenService.GetRefreshTokenAsync(customer.CustomerID);


            if (customer == null)
                return Unauthorized("Invalid refresh request");

            if (RefreshToken == null || RefreshToken.Any(rt => rt.RefreshTokenRevokedAt != null))
                return Unauthorized("Refresh token is revoked");

            if (RefreshToken == null || RefreshToken.Any(rt => rt.RefreshTokenExpiresAt == null || rt.RefreshTokenExpiresAt <= DateTime.UtcNow))
                return Unauthorized("Refresh token expired");

            bool refreshValid = false;
            foreach (var rt in RefreshToken)
            {
                if (BCrypt.Net.BCrypt.Verify(request.RefreshToken, rt.TokenHash))
                {
                    refreshValid = true;
                    break;
                }
            }
            if (!refreshValid)
                return Unauthorized("Invalid refresh token");

            // Issue NEW access token (same claims & signing settings as login)
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, customer.CustomerID.ToString()),
        new Claim(ClaimTypes.Name, customer.Username),
        new Claim(ClaimTypes.Role, customer.Role ? "Admin" : "User")
    };

            

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "ShopSphereApi",
                audience: "ShopSphereApiUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            var newAccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Rotation: replace refresh token
            var newRefreshToken = GenerateRefreshToken();
            AddRefreshTokenDto addRefreshTokenDto = new AddRefreshTokenDto
            {
                CustomerID = customer.CustomerID,
                TokenHash = BCrypt.Net.BCrypt.HashPassword(newRefreshToken),
                RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };

            await _refreshTokenService.AddRefreshTokenAsync(addRefreshTokenDto);

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var customer = await _customerService.GetByUserNameAsync(request.UserName);
            IEnumerable<RefreshTokenDto> RefreshToken = await _refreshTokenService.GetRefreshTokenAsync(customer.CustomerID);

            if (customer == null)
                return Ok(); // Do not reveal if user exists

            bool refreshValid = false;
            int RefreshTokenIdToRevoke = -1;
            foreach (var rt in RefreshToken)
            {
                if (BCrypt.Net.BCrypt.Verify(request.RefreshToken, rt.TokenHash))
                {
                    refreshValid = true;
                    RefreshTokenIdToRevoke = rt.RefreshTokenID;
                    break;
                }
            }
            if (!refreshValid)
                return Ok();

            await _refreshTokenService.RevokeRefreshTokenAsync(RefreshTokenIdToRevoke);
            return Ok("Logged out successfully");
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

    }
}
