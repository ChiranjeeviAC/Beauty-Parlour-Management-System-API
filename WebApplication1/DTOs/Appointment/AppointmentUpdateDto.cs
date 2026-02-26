using System.ComponentModel.DataAnnotations;
using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs.Appointment
{
    public class AppointmentUpdateDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }
    }
}