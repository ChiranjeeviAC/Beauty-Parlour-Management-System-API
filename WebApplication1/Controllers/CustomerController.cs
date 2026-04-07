using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.DTOs;
using WebApplication1.DTOs.Customer;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (id != userId)
                return Unauthorized("You can access only your data");

            var result = _service.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.Update(id, dto);

            if (result == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(new
            {
                message = "Customer updated successfully",
                data = result
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var success = _service.Delete(id);

            if (!success)
                return NotFound(new { message = "Customer not found" });

            return Ok(new { message = "Customer deleted successfully" });
        }

        [HttpPut("{id}/change-password")]
        public IActionResult ChangePassword(int id, ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _service.ChangePassword(id, dto);

            if (!success)
                return BadRequest(new { message = "Invalid old password or user not found" });

            return Ok(new { message = "Customer password updated successfully" });
        }

        [HttpGet("{id}/appointments")]
        public IActionResult GetCustomerAppointments(int id)
        {
            var result = _service.GetCustomerAppointments(id);

            if (result == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(result);
        }
    }
}