using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs.Appointment;
using WebApplication1.Model;
using WebApplication1.Model.Enums;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            var appointments = _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Staff)
                .Include(a => a.Service)
                .Include(a => a.Payment)
                .ToList();

            var result = appointments.Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                CustomerId = a.CustomerId,
                CustomerName = a.Customer.Name,

                StaffId = a.StaffId,
                StaffName = a.Staff.StaffName,

                ServiceId = a.ServiceId,
                ServiceName = a.Service.ServiceName,
                ServicePrice = a.Service.Price,

                AppointmentDate = a.AppointmentDate,
                TimeSlot = a.TimeSlot,
                Status = a.Status,
                IsPaymentDone = a.Payment != null
            }).ToList();

            return Ok(result);
        }

        // 🔹 GET BY ID
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Staff)
                .Include(a => a.Service)
                .Include(a => a.Payment)
                .FirstOrDefault(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            var result = new AppointmentResponseDto
            {
                AppointmentId = appointment.AppointmentId,
                CustomerId = appointment.CustomerId,
                CustomerName = appointment.Customer.Name,

                StaffId = appointment.StaffId,
                StaffName = appointment.Staff.StaffName,

                ServiceId = appointment.ServiceId,
                ServiceName = appointment.Service.ServiceName,
                ServicePrice = appointment.Service.Price,

                AppointmentDate = appointment.AppointmentDate,
                TimeSlot = appointment.TimeSlot,
                Status = appointment.Status,
                IsPaymentDone = appointment.Payment != null
            };

            return Ok(result);
        }

        // 🔹 BOOK APPOINTMENT
        [HttpPost]
        public IActionResult BookAppointment(AppointmentCreateDto dto)
        {
            var isAlreadyBooked = _context.Appointments.Any(a =>
            a.StaffId == dto.StaffId &&
            a.AppointmentDate.Date == dto.AppointmentDate.Date &&
            a.TimeSlot == dto.TimeSlot &&
            a.Status != AppointmentStatus.Cancelled);

            if (isAlreadyBooked)
            {
                return BadRequest(new
                {
                    message = "Staff is already booked for this time slot"
                });
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _context.Customers.Find(dto.CustomerId);
            var staff = _context.Staffs.Find(dto.StaffId);
            var service = _context.Services.Find(dto.ServiceId);

            if (customer == null)
                return BadRequest(new { message = "Invalid CustomerId" });

            if (staff == null)
                return BadRequest(new { message = "Invalid StaffId" });

            if (service == null)
                return BadRequest(new { message = "Invalid ServiceId" });

            var appointment = new Appointment
            {
                CustomerId = dto.CustomerId,
                StaffId = dto.StaffId,
                ServiceId = dto.ServiceId,
                AppointmentDate = dto.AppointmentDate,
                TimeSlot = dto.TimeSlot,
                Status = AppointmentStatus.Booked
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Appointment booked successfully",
                data = appointment.AppointmentId
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id}")]
        public IActionResult UpdateAppointment(int id, AppointmentUpdateDto dto)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });


            var isAlreadyBooked = _context.Appointments.Any(a =>
            a.StaffId == appointment.StaffId &&
            a.AppointmentDate.Date == appointment.AppointmentDate.Date &&
            a.TimeSlot == appointment.TimeSlot &&
            a.Status != AppointmentStatus.Cancelled);

            if (isAlreadyBooked)
            {
                return BadRequest(new
                {
                    message = "Staff is already booked for this time slot you cannot update"
                });
            }



            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.TimeSlot = dto.TimeSlot;
            

            _context.SaveChanges();

            return Ok(new { message = "Appointment updated successfully" });
        }

        // To change appointment Time Slot

        [HttpPut("{id}")]
        public IActionResult UpdateAppointmentTimeslot(int id, string TimeSlot)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            var isAlreadyBooked = _context.Appointments.Any(a =>
            a.StaffId == appointment.StaffId &&
            a.AppointmentDate.Date == appointment.AppointmentDate.Date &&
            a.TimeSlot == appointment.TimeSlot &&
            a.Status != AppointmentStatus.Cancelled);

            if (isAlreadyBooked)
            {
                return BadRequest(new
                {
                    message = "Staff is already booked for this time slot you cannot update"
                });
            }


            appointment.TimeSlot = TimeSlot;
           

            _context.SaveChanges();

            return Ok(new { message = $"Appointment time updated to {TimeSlot} successfully" });
        }


        // To change appointment Date

        [HttpPut("{id}")]
        public IActionResult UpdateAppointmentDate(int id, DateTime AppointmentDate)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            var isAlreadyBooked = _context.Appointments.Any(a =>
            a.StaffId == appointment.StaffId &&
            a.AppointmentDate.Date == appointment.AppointmentDate.Date &&
            a.TimeSlot == appointment.TimeSlot &&
            a.Status != AppointmentStatus.Cancelled);

            if (isAlreadyBooked)
            {
                return BadRequest(new
                {
                    message = "Staff is already booked for this time slot you cannot update"
                });
            }


            appointment.AppointmentDate = AppointmentDate;


            _context.SaveChanges();

            return Ok(new { message = $"Appointment time updated to {AppointmentDate} successfully" });
        }


        // 🔹 CANCEL
        [HttpDelete("{id}")]
        public IActionResult CancelAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
                return NotFound(new { message = "Appointment not found" });

            appointment.Status = AppointmentStatus.Cancelled;
            _context.SaveChanges();

            return Ok(new { message = "Appointment cancelled successfully" });
        }

        // 🔹 GET: api/appointment/daily-count
        [HttpGet("daily-count")]
        public IActionResult GetDailyAppointmentCount()
        {
            var dailyCount = _context.Appointments
                .AsEnumerable()
                .GroupBy(a => a.AppointmentDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalAppointments = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Ok(dailyCount);
        }
    }
}