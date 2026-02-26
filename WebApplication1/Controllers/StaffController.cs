using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs.Staff;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  GET: api/staff
        [HttpGet]
        public IActionResult GetAllStaff()
        {
            var staffList = _context.Staffs.ToList();

            var result = staffList.Select(s => new StaffResponseDto
            {
                StaffId = s.StaffId,
                StaffName = s.StaffName,
                Role = s.Role,
                Phone = s.Phone,
                Experience = s.Experience,
                Salary = s.Salary
            }).ToList();

            return Ok(result);
        }

        // GET: api/staff/5
        [HttpGet("{id}")]
        public IActionResult GetStaffById(int id)
        {
            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound(new { message = "Staff not found" });

            var result = new StaffResponseDto
            {
                StaffId = staff.StaffId,
                StaffName = staff.StaffName,
                Role = staff.Role,
                Phone = staff.Phone,
                Experience = staff.Experience,
                Salary = staff.Salary
            };

            return Ok(result);
        }

        

        // PUT: api/staff/5
        [HttpPut("{id}")]
        public IActionResult UpdateStaff(int id, StaffUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound(new { message = "Staff not found" });

            staff.StaffName = dto.StaffName;
            staff.Role = dto.Role;
            staff.Phone = dto.Phone;
            staff.Experience = dto.Experience;
            staff.Salary = dto.Salary;

            _context.SaveChanges();

            var result = new StaffResponseDto
            {
                StaffId = staff.StaffId,
                StaffName = staff.StaffName,
                Role = staff.Role,
                Phone = staff.Phone,
                Experience = staff.Experience,
                Salary = staff.Salary
            };

            return Ok(new
            {
                message = "Staff updated successfully",
                data = result
            });
        }

        // DELETE: api/staff/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound(new { message = "Staff not found" });

            _context.Staffs.Remove(staff);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Staff deleted successfully",
                data = new StaffResponseDto
                {
                    StaffId = staff.StaffId,
                    StaffName = staff.StaffName,
                    Role = staff.Role,
                    Phone = staff.Phone,
                    Experience = staff.Experience,
                    Salary = staff.Salary
                }
            });
        }

        [HttpGet("{id}/appointments")]
        public IActionResult GetStaffAppointments(int id)
        {
            var staff = _context.Staffs
                .Include(s => s.Appointments)
                .ThenInclude(a => a.Service)
                .FirstOrDefault(s => s.StaffId == id);

            if (staff == null)
                return NotFound(new { message = "Staff not found" });

            var result = staff.Appointments.Select(a => new
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