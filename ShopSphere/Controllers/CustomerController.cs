using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.Services;
using ShopSphere.Models.Entities;

namespace ShopSphere.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ShopSphere/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetById/{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("GetAll", Name = "GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomerRequestDto getAllCustomer)
        {
            try
            {

                if (getAllCustomer.SortOrder != "ASC" && getAllCustomer.SortOrder != "DESC")
                    return BadRequest("SortOrder must be either 'asc' or 'desc'.");
                if (getAllCustomer.SortBy != "CustomerID" && getAllCustomer.SortBy != "Name" && getAllCustomer.SortBy != "Email"
                    && getAllCustomer.SortBy != "Phone" && getAllCustomer.SortBy != "Address" && getAllCustomer.SortBy != "Username"
                    && getAllCustomer.SortBy != "Balance" && getAllCustomer.SortBy != "Role" && getAllCustomer.SortBy != "CreatedDate" && getAllCustomer.SortBy != "IsActive")
                    return BadRequest("SortBy must be on of That List { 'CustomerID', 'Name', 'Email' 'Phone', 'Address', 'Username' 'Balance', 'Role', 'CreatedDate','IsActive'.}");

                var customers = await _customerService.GetAllCustomersAsync(getAllCustomer);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetCustomerCountAsync",Name = "GetCustomerCountAsync")]
        public async Task<IActionResult> GetCustomerCountAsync([FromQuery] CustomerCountDto countDto)
        {
            var count = await _customerService.GetCustomerCountAsync(countDto);
            return Ok(count);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByUserName/{username}", Name = "GetByUserName")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var customer = await _customerService.GetByUserNameAsync(username);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpGet("status/{id}", Name = "GetCustomerStatus")]
        public async Task<IActionResult> GetCustomerStatus(int id)
        {
            var status = await _customerService.GetCustomerStatusAsync(id);
            if (status == null)
                return NotFound();
            return Ok(status);
        }
[AllowAnonymous]
        [HttpPost("register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                registerDto.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
                var newId = await _customerService.RegisterAsync(registerDto);
                return CreatedAtRoute("GetById", new { id = newId }, newId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("update/{id}", Name = "Update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDto updateDto)
        {
            try
            {
                var result = await _customerService.UpdateCustomerAsync(id, updateDto);
                if (result == 0)
                    return NotFound();
                else
                    return Ok($"{result} rows of Customer  updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("changepassword/{id}", Name = "ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                var User= await _customerService.GetByUserNameAsync(customer.Username);
                    if (User == null || User.CustomerID != id)
                        return BadRequest("Unauthorized to change password for this user");
                    if(!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, User.Password))
                        return BadRequest("Current password is incorrect");

                    changePasswordDto.NewPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
                var result = await _customerService.ChangePasswordAsync(id, changePasswordDto);
                if (!result)
                    return BadRequest("Password change failed");
                return Ok("Password changed successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("changerole/{adminId}/{customerId}", Name = "ChangeCustomerRole")]
        public async Task<IActionResult> ChangeCustomerRole(int adminId, int customerId, [FromBody] byte newRole)
        {
            try
            {
                var result = await _customerService.ChangeCustomerRoleAsync(adminId, customerId, newRole);
                if (!result)
                    return BadRequest("Role change failed");
                return Ok("Role changed successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}", Name = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                return Ok($"Customer with ID {id} deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 
        
    }
}
