using Microsoft.AspNetCore.Identity;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public List<CustomerResponseDto> GetAll()
        {
            var customers = _repository.GetAll();

            return customers.Select(c => new CustomerResponseDto
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Address = c.Address,
                Gender = c.Gender
            }).ToList();
        }

        public CustomerResponseDto GetById(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null) return null;

            return new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                Gender = customer.Gender
            };
        }

        public CustomerResponseDto Update(int id, CustomerUpdateDto dto)
        {
            var customer = _repository.GetById(id);
            if (customer == null) return null;

            customer.Name = dto.Name;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;
            customer.Address = dto.Address;
            customer.Gender = dto.Gender;

            var updated = _repository.Update(customer);

            return new CustomerResponseDto
            {
                CustomerId = updated.CustomerId,
                Name = updated.Name,
                Phone = updated.Phone,
                Email = updated.Email,
                Address = updated.Address,
                Gender = updated.Gender
            };
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public bool ChangePassword(int id, ChangePasswordDto dto)
        {
            var user = _repository.GetUserByCustomerId(id);
            if (user == null) return false;

            var hasher = new PasswordHasher<UserC>();

            var result = hasher.VerifyHashedPassword(
                user,
                user.Password,
                dto.OldPassword
            );

            if (result == PasswordVerificationResult.Failed)
                return false;

            user.Password = hasher.HashPassword(user, dto.NewPassword);

            _repository.UpdateUser(user);

            return true;
        }

        public object GetCustomerAppointments(int id)
        {
            var customer = _repository.GetCustomerWithAppointments(id);
            if (customer == null) return null;

            return customer.Appointments.Select(a => new
            {
                a.AppointmentId,
                a.AppointmentDate,
                a.TimeSlot,
                ServiceName = a.Service.ServiceName,
                a.Status
            }).ToList();
        }
    }
}