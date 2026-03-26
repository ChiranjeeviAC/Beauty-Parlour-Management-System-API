using Microsoft.AspNetCore.Identity;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public class AuthCustomerService : IAuthCustomerService
    {
        private readonly IAuthCustomerRepository _repository;

        public AuthCustomerService(IAuthCustomerRepository repository)
        {
            _repository = repository;
        }

        public object Register(CustomerRegisterDto dto)
        {
            // Check email
            if (_repository.EmailExists(dto.Email))
            {
                return new { success = false, message = "Email already exists" };
            }

            // Create Customer
            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Address = dto.Address,
                Gender = dto.Gender,
                Email = dto.Email
            };

            var savedCustomer = _repository.AddCustomer(customer);

            // Create UserC (login table)
            var user = new UserC
            {
                Email = dto.Email,
                CustomerId = savedCustomer.CustomerId
            };

            var hasher = new PasswordHasher<UserC>();
            user.Password = hasher.HashPassword(user, dto.Password);

            _repository.AddUser(user);

            return new
            {
                success = true,
                message = "Customer registered successfully",
                customerId = savedCustomer.CustomerId
            };
        }

        public object Login(LoginDto dto)
        {
            var customer = _repository.GetCustomerByEmail(dto.Email);

            if (customer == null)
            {
                return new { success = false, message = "Invalid email or password" };
            }

            var user = _repository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return new { success = false, message = "Invalid email or password" };
            }

            var hasher = new PasswordHasher<UserC>();

            var result = hasher.VerifyHashedPassword(
                user,
                user.Password,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return new { success = false, message = "Invalid email or password" };
            }

            return new
            {
                success = true,
                message = "Customer login successful",
                customerId = customer.CustomerId
            };
        }
    }
}