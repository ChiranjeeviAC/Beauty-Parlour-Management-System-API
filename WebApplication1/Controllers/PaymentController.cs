using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Model;
using WebApplication1.Model.Enums;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET ALL PAYMENTS
        [HttpGet]
        public IActionResult GetPayments()
        {
            var payments = _context.Payments
                .Include(p => p.Appointment)
                .ThenInclude(a => a.Customer)
                .Include(p => p.Appointment)
                .ThenInclude(a => a.Service)
                .ToList();

            var result = payments.Select(p => new PaymentResponseDto
            {
                PaymentId = p.PaymentId,
                AppointmentId = p.AppointmentId,
                CustomerName = p.Appointment.Customer.Name,
                ServiceName = p.Appointment.Service.ServiceName,
                Amount = p.Amount,
                PaymentMode = p.PaymentMode,
                PaymentDate = p.PaymentDate
            }).ToList();

            return Ok(result);
        }

        // 🔹 GET PAYMENT BY ID
        [HttpGet("{id}")]
        public IActionResult GetPaymentById(int id)
        {
            var payment = _context.Payments
                .Include(p => p.Appointment)
                .ThenInclude(a => a.Customer)
                .Include(p => p.Appointment)
                .ThenInclude(a => a.Service)
                .FirstOrDefault(p => p.PaymentId == id);

            if (payment == null)
                return NotFound(new { message = "Payment not found" });

            return Ok(new PaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                AppointmentId = payment.AppointmentId,
                CustomerName = payment.Appointment.Customer.Name,
                ServiceName = payment.Appointment.Service.ServiceName,
                Amount = payment.Amount,
                PaymentMode = payment.PaymentMode,
                PaymentDate = payment.PaymentDate
            });
        }

        // 🔹 ADD PAYMENT
        [HttpPost]
        public IActionResult AddPayment(PaymentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = _context.Appointments
                .Include(a => a.Service)
                .FirstOrDefault(a => a.AppointmentId == dto.AppointmentId);

            if (appointment == null)
                return BadRequest(new { message = "Invalid AppointmentId" });

            // Prevent duplicate payment
            var existingPayment = _context.Payments
                .Any(p => p.AppointmentId == dto.AppointmentId);

            if (existingPayment)
                return BadRequest(new { message = "Payment already exists for this appointment" });

            var payment = new Payment
            {
                AppointmentId = dto.AppointmentId,
                Amount = appointment.Service.Price, // 🔥 Auto amount
                PaymentMode = dto.PaymentMode,
                PaymentDate = DateTime.Now
            };

            _context.Payments.Add(payment);

            // Update appointment status
            appointment.Status = AppointmentStatus.Completed;

            _context.SaveChanges();

            return Ok(new
            {
                message = "Payment added successfully",
                data = payment.PaymentId
            });
        }
    }
}