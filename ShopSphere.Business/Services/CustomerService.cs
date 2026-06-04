using AutoMapper;
using ShopSphere.Business.DTOs.Customer;
using ShopSphere.Business.Helpers;
using ShopSphere.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _unitOfWork.Customers.GetbyIDAsync(customerId);
            return _mapper.Map<CustomerDto>(customer);
        }
        public async Task<UserDto> GetByUserNameAsync(string username)
        {
            var customer = await _unitOfWork.Customers.GetByUserNameAsync(username);
            return _mapper.Map<UserDto>(customer);
        }
        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(GetAllCustomerRequestDto getAllCustomer)
        {
            var customers = await _unitOfWork.Customers.GetAllAsync(_mapper.Map<GetAllCustomerRequest>(getAllCustomer));
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
        public Task<int> GetCustomerCountAsync(CustomerCountDto customerCount)
        {
            return _unitOfWork.Customers.GetCountAsync(customerCount.SearchTerm, customerCount.SearchColumn);
        }

        public async Task<int> RegisterAsync(RegisterDto registerDto)
        {
            // Check if username exists
            var existing = await _unitOfWork.Customers.GetByUserNameAsync(registerDto.Username);
            if (existing != null)
                throw new Exception("Username already exists");

            var customer = new Customer
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Address = registerDto.Address,
                Username = registerDto.Username,
                Password = registerDto.Password, // Will hash later
                Balance = 0,
                Role = false,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            return await _unitOfWork.Customers.AddAsync(customer);
        }

        public async Task<CustomerDto> LoginAsync(LoginDto loginDto)
        {
            var customer = await _unitOfWork.Customers.LoginAsync(
                loginDto.Username,
                loginDto.Password
            );

            if (customer == null)
                throw new Exception("Invalid username or password");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<int> UpdateCustomerAsync(int customerId, UpdateDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.CustomerID = customerId;
            return await _unitOfWork.Customers.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _unitOfWork.Customers.DeleteAsync(customerId);
        }

        public async Task<bool> ChangePasswordAsync(int customerId, ChangePasswordDto changePasswordDto)
        {
            return await _unitOfWork.Customers.ChangePasswordAsync(customerId,  changePasswordDto.NewPassword);
        }
        public async Task<bool> ChangeCustomerRoleAsync(int adminId, int customerId, byte newRole)
        {
            // Check if adminId belongs to an admin
            var adminStatus = await _unitOfWork.Customers.GetCustomerStatusAsync(adminId);
            if (adminStatus == null || !adminStatus.IsAdmin) 
                throw new Exception("Only admins can change roles");
            return await _unitOfWork.Customers.ChangeCustmoerRoleAsync(adminId, customerId, newRole);

        }
        public async Task<CustomerStatus> GetCustomerStatusAsync(int customerId)
        {
            return await _unitOfWork.Customers.GetCustomerStatusAsync(customerId);
        }
       

    }
}

