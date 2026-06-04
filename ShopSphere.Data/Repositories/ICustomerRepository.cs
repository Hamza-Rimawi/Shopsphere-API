using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<CustomerWithoutPassword> GetbyIDAsync(int customerId);
        Task<IEnumerable<CustomerWithoutPassword>> GetAllAsync(GetAllCustomerRequest getAllCustomer);
        Task<int> GetCountAsync( string? SearchTerm= null, string ? SearchColumn = null);

        Task<int> AddAsync(Customer customer);
        Task<int> UpdateAsync(Customer customer);
        Task<int> DeleteAsync(int customerId);
        Task<Customer> GetByUserNameAsync(string username);
        Task<CustomerWithoutPassword> LoginAsync(string username, string password);
        Task<CustomerStatus> GetCustomerStatusAsync(int customerId);
        Task<bool> ChangePasswordAsync(int customerId,  string newPassword);
        Task<bool> ChangeCustmoerRoleAsync(int adminId, int customerId,  byte newRole);

        

    }
}
