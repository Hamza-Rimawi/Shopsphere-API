using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(GetAllCustomerRequestDto getAllCustomer);
        Task<int> GetCustomerCountAsync(CustomerCountDto customerCount);

        Task<int> RegisterAsync(RegisterDto registerDto);
        Task<CustomerDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetByUserNameAsync(string username);
        Task <int> UpdateCustomerAsync(int customerId, UpdateDto updateDto);
        Task DeleteCustomerAsync(int customerId);
        Task<bool> ChangePasswordAsync(int customerId, ChangePasswordDto changePasswordDto);
        Task<bool> ChangeCustomerRoleAsync(int adminId, int customerId, byte newRole);
       
        Task<CustomerStatus> GetCustomerStatusAsync(int customerId);

    }
}
