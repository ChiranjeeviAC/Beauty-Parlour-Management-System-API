using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Model.Enums;

namespace WebApplication1.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public PaymentMode PaymentMode { get; set; }

        public DateTime PaymentDate { get; set; }

        // Foreign Key
        public int AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
    }
}