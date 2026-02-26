using System.ComponentModel.DataAnnotations;
using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs.Payment
{
    public class PaymentCreateDto
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public PaymentMode PaymentMode { get; set; }
    }
}