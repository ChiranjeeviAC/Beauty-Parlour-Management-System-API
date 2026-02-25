using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Model.Enums;

namespace WebApplication1.Model
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        [Required]
        public AppointmentStatus Status { get; set; }

        // Foreign Keys
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public int ServiceId { get; set; }

        // Navigation Properties
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        // One-to-One Relationship
        public Payment Payment { get; set; }
    }
}