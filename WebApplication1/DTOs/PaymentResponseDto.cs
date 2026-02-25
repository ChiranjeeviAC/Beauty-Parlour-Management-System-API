using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }

        public int AppointmentId { get; set; }

        public string CustomerName { get; set; }
        public string ServiceName { get; set; }

        public double Amount { get; set; }

        public PaymentMode PaymentMode { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}