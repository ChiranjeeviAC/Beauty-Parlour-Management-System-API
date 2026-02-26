using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/auth-staff")]
    [ApiController]
    public class AuthStaffController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthStaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 REGISTER STAFF
        [HttpPost("register")]
        public IActionResult Register(StaffRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check email exists
            var emailExists = _context.UserSs
                .Any(u => u.Email == dto.Email);

            if (emailExists)
                return BadRequest(new { message = "Email already exists" });

            // Create Staff (no password here)
            var staff = new Staff
            {
                StaffName = dto.StaffName,
                Role = dto.Role,
                Phone = dto.Phone,
                Experience = dto.Experience,
                Salary = dto.Salary,
                Email = dto.Email
            };

            _context.Staffs.Add(staff);
            _context.SaveChanges();

            // Store login details separately
            var user = new UserS
            {
                Email = dto.Email,
                Password = dto.Password,
                StaffId = staff.StaffId
            };

            _context.UserSs.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Staff registered successfully",
                staffId = staff.StaffId
            });
        }

        // 🔹 LOGIN STAFF
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.UserSs
                .FirstOrDefault(u =>
                    u.Email == dto.Email &&
                    u.Password == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new
            {
                message = "Staff login successful",
                staffId = user.StaffId
            });
        }
    }
}