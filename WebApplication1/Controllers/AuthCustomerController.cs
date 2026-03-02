using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Migrations;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/auth-customer")]
    [ApiController]
    public class AuthCustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthCustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  REGISTER CUSTOMER
        [HttpPost("register")]
        public IActionResult Register(CustomerRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if email already exists
            var emailExists = _context.UserCs
                .Any(u => u.Email == dto.Email);

            if (emailExists)
                return BadRequest(new { message = "Email already exists" });

            // Create Customer (without password)
            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Address = dto.Address,
                Gender = dto.Gender,
                Email = dto.Email,
                
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            var hasher = new PasswordHasher<Customer>();

            // Store login details separately
            var user = new UserC
            {
                Email = dto.Email,
                
                CustomerId = customer.CustomerId
            };

            user.Password = hasher.HashPassword(customer, user.Password);

            _context.UserCs.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Customer registered successfully",
                customerId = customer.CustomerId
            });
        }

        //  LOGIN CUSTOMER
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var user = _context.Customers
               .FirstOrDefault(x => x.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid Employee ID or Password");
            }



            

            var userC = _context.UserCs
                .FirstOrDefault(u =>
                    u.Email == dto.Email );

            var hasher = new PasswordHasher<Customer>();

            var result = hasher.VerifyHashedPassword(
                user,
                userC.Password,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid email or password" });


            return Ok(new
            {
                message = "Customer login successful",
                customerId = user.CustomerId
            });
        }
    }
}