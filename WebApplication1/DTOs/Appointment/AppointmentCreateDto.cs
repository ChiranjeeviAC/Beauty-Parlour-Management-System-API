using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Appointment
{
    public class AppointmentCreateDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int StaffId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string TimeSlot { get; set; }
    }
}