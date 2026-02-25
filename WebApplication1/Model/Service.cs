using System.ComponentModel.DataAnnotations;
using WebApplication1.Model.Enums;

namespace WebApplication1.Model
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [Required]
        public ServiceCategory Category { get; set; }  // Enum

        [Required]
        public double Price { get; set; }

        [Required]
        public int Duration { get; set; }   // Minutes

        // Navigation Property (1 Service → Many Appointments)
        public ICollection<Appointment> Appointments { get; set; }
    }
}