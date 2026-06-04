using Microsoft.Data.SqlClient;
using ShopSphere.Business.Database;
using ShopSphere.Data.Repositories;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly SqlDataAccess _sqlDataAccess;

        public CustomerRepository(SqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public Task<CustomerWithoutPassword> GetbyIDAsync(int customerId)
        {
            return _sqlDataAccess.LoadDataAsync<CustomerWithoutPassword, dynamic>("SP_GetCustomerByID", new { CustomerId = customerId })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }
        public Task<IEnumerable<CustomerWithoutPassword>> GetAllAsync(GetAllCustomerRequest getAllCustomer)
        {
            return _sqlDataAccess.LoadDataAsync<CustomerWithoutPassword, dynamic>("SP_GetAllCustomers", getAllCustomer);
        }
        public Task<int> GetCountAsync(string? SearchTerm= null, string? SearchColumn=null)
        {
            var parameters = new
            {
                searchTerm = SearchTerm,
                searchColumn = SearchColumn
            };
            return _sqlDataAccess.ExecuteScalarAsync<int, dynamic>("SP_GetCustomerCount", parameters);
        }

        public Task<int> AddAsync(Customer customer)
        {
            var parameters = new
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Username = customer.Username,
                Password = customer.Password,
                Role = customer.Role
            };
            return _sqlDataAccess.SaveDataAsync("SP_AddNewCustomer", parameters, "NewCustomerID");
        }
        public async Task<int> UpdateAsync(Customer customer)
        {
            using var connection = new SqlConnection(_sqlDataAccess._ConnectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("SP_UpdateCustomer", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CustomerId", customer.CustomerID);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Email", customer.Email);
            command.Parameters.AddWithValue("@Phone", customer.Phone);
            command.Parameters.AddWithValue("@Address", customer.Address);
            command.Parameters.AddWithValue("@Username", customer.Username);
            
            return await command.ExecuteNonQueryAsync();
            
        }
        public Task<int> DeleteAsync(int customerId)
        {
            return _sqlDataAccess.SaveDataAsync("SP_DeleteCustomer", new { CustomerId = customerId });
        }
        public Task<Customer> GetByUserNameAsync(string username)
        {
            return _sqlDataAccess.LoadDataAsync<Customer, dynamic>("SP_GetCustomerByUserName", new { Username = username })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }
        public Task<CustomerWithoutPassword> LoginAsync(string username, string password)
        {
           
            return _sqlDataAccess.LoadDataAsync<CustomerWithoutPassword, dynamic>("SP_CustomerLogin", new { Username = username, Password = password })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }
        public Task<CustomerStatus> GetCustomerStatusAsync(int customerId)
        {
            return _sqlDataAccess.LoadDataAsync<CustomerStatus, dynamic>("SP_GetCustomerStatus", new { CustomerId = customerId })
                .ContinueWith(task => task.Result.FirstOrDefault());
        }

        public Task<bool> ChangePasswordAsync(int customerId,  string newPassword)
        {
            return _sqlDataAccess.ExecuteScalarAsync<bool, dynamic>("SP_ChangeCustomerPassword", new { CustomerId = customerId, NewPassword = newPassword }, "Success");
        }
        public Task<bool> ChangeCustmoerRoleAsync(int adminId, int customerId, byte newRole)
        {
            return _sqlDataAccess.ExecuteScalarAsync<bool, dynamic>("SP_ChangeCustomerRole", new { AdminId = adminId, CustomerId = customerId, NewRole = newRole }, "Success");
        }
      
    }
}
