using Microsoft.AspNetCore.Identity;
using WebApplication1.DTOs;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public class AuthStaffService : IAuthStaffService
    {
        private readonly IAuthStaffRepository _repository;

        public AuthStaffService(IAuthStaffRepository repository)
        {
            _repository = repository;
        }


        private readonly IJwtService _jwtService;

        public AuthStaffService(IAuthStaffRepository repository, IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        public object Register(StaffRegisterDto dto)
        {
            if (_repository.EmailExists(dto.Email))
            {
                return new { success = false, message = "Email already exists" };
            }

            var staff = new Staff
            {
                StaffName = dto.StaffName,
                Role = dto.Role,
                Phone = dto.Phone,
                Experience = dto.Experience,
                Salary = dto.Salary,
                Email = dto.Email
            };

            var savedStaff = _repository.AddStaff(staff);

            var user = new UserS
            {
                Email = dto.Email,
                StaffId = savedStaff.StaffId
            };

            var hasher = new PasswordHasher<UserS>();
            user.Password = hasher.HashPassword(user, dto.Password);

            _repository.AddUser(user);

            return new
            {
                success = true,
                message = "Staff registered successfully",
                staffId = savedStaff.StaffId
            };
        }

        public object Login(LoginDto dto)
        {
            var staff = _repository.GetStaffByEmail(dto.Email);

            if (staff == null)
            {
                return new { success = false, message = "Invalid email or password" };
            }

            var user = _repository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return new { success = false, message = "Invalid email or password" };
            }

            var hasher = new PasswordHasher<UserS>();

            var result = hasher.VerifyHashedPassword(
                user,
                user.Password,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return new { success = false, message = "Invalid email or password" };
            }

            var token = _jwtService.GenerateToken(
    staff.StaffId,
    staff.Email,
    staff.Role.ToString()   // e.g., Admin / Staff
);
            return new
            {
                success = true,
                message = "Staff login successful",
                token = token,
                staffId = staff.StaffId,
                role = staff.Role
            };
        }
    }
}