using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/customer
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _context.Customers.ToList();

            var result = customers.Select(c => new CustomerResponseDto
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                Address = c.Address,
                Gender = c.Gender
            }).ToList();

            return Ok(result);
        }

        // GET: api/customer/5
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            var result = new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                Gender = customer.Gender
            };

            return Ok(result);
        }

        //  POST: api/customer
        [HttpPost]
        public IActionResult AddCustomer(CustomerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                Gender = dto.Gender
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            var result = new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                Gender = customer.Gender
            };

            return CreatedAtAction(nameof(GetCustomerById),
                new { id = customer.CustomerId }, result);
        }

        //  PUT: api/customer/5
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            customer.Name = dto.Name;
            customer.Phone = dto.Phone;
            customer.Email = dto.Email;
            customer.Address = dto.Address;
            customer.Gender = dto.Gender;

            _context.SaveChanges();

            var result = new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                Gender = customer.Gender
            };

            return Ok(new
            {
                message = "Customer updated successfully",
                data = result
            });
        }

        //  DELETE: api/customer/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Customer deleted successfully",
                data = new CustomerResponseDto
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email,
                    Address = customer.Address,
                    Gender = customer.Gender
                }


            });
        }


        [HttpGet("{id}/appointments")]
        public IActionResult GetCustomerAppointments(int id)
        {
            var customer = _context.Customers
                .Include(c => c.Appointments)
                .ThenInclude(a => a.Service)
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            var result = customer.Appointments.Select(a => new
            {
                a.AppointmentId,
                a.AppointmentDate,
                a.TimeSlot,
                ServiceName = a.Service.ServiceName,
                a.Status
            }).ToList();

            return Ok(result);
        }
    }
}