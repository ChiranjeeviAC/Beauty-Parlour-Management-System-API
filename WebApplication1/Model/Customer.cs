using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required]
        public string Gender { get; set; }

        // Navigation Property (1 Customer → Many Appointments)
        public ICollection<Appointment> Appointments { get; set; }
    }
}