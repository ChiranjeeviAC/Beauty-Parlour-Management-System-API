using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs.Service;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  GET: api/service
        [HttpGet]
        public IActionResult GetAllServices()
        {
            var services = _context.Services.ToList();

            var result = services.Select(s => new ServiceResponseDto
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                Category = s.Category,
                Price = s.Price,
                Duration = s.Duration
            }).ToList();

            return Ok(result);
        }

        //  GET: api/service/5
        [HttpGet("{id}")]
        public IActionResult GetServiceById(int id)
        {
            var service = _context.Services.Find(id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            var result = new ServiceResponseDto
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Category = service.Category,
                Price = service.Price,
                Duration = service.Duration
            };

            return Ok(result);
        }

        //  POST: api/service
        [HttpPost]
        public IActionResult AddService(ServiceCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = new Service
            {
                ServiceName = dto.ServiceName,
                Category = dto.Category,
                Price = dto.Price,
                Duration = dto.Duration
            };

            _context.Services.Add(service);
            _context.SaveChanges();

            var result = new ServiceResponseDto
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Category = service.Category,
                Price = service.Price,
                Duration = service.Duration
            };

            return CreatedAtAction(nameof(GetServiceById),
                new { id = service.ServiceId }, result);
        }

        //  PUT: api/service/5
        [HttpPut("{id}")]
        public IActionResult UpdateService(int id, ServiceUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = _context.Services.Find(id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            service.ServiceName = dto.ServiceName;
            service.Category = dto.Category;
            service.Price = dto.Price;
            service.Duration = dto.Duration;

            _context.SaveChanges();

            var result = new ServiceResponseDto
            {
                ServiceId = service.ServiceId,
                ServiceName = service.ServiceName,
                Category = service.Category,
                Price = service.Price,
                Duration = service.Duration
            };

            return Ok(new
            {
                message = "Service updated successfully",
                data = result
            });
        }

        //  DELETE: api/service/5
        [HttpDelete("{id}")]
        public IActionResult DeleteService(int id)
        {
            var service = _context.Services.Find(id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            _context.Services.Remove(service);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Service deleted successfully",
                data = new ServiceResponseDto
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    Category = service.Category,
                    Price = service.Price,
                    Duration = service.Duration
                }
            });
        }

        [HttpGet("{id}/appointments")]
        public IActionResult GetServiceAppointments(int id)
        {
            var service = _context.Services
                .Include(s => s.Appointments)
                .ThenInclude(a => a.Customer)
                .FirstOrDefault(s => s.ServiceId == id);

            if (service == null)
                return NotFound(new { message = "Service not found" });

            var result = service.Appointments.Select(a => new
            {
                a.AppointmentId,
                a.AppointmentDate,
                a.TimeSlot,
                CustomerName = a.Customer.Name,
                a.Status
            }).ToList();

            return Ok(result);
        }
    }
}