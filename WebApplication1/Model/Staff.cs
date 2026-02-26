using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApplication1.Model.Enums;

namespace WebApplication1.Model
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StaffName { get; set; }

        [Required]
        public StaffRole Role { get; set; }   // Enum

        [Required]
        [Phone]
        public string Phone { get; set; }

        public int Experience { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public double Salary { get; set; }

        // Navigation Property (1 Staff → Many Appointments)
        
        public ICollection<Appointment> Appointments { get; set; }
    }
}