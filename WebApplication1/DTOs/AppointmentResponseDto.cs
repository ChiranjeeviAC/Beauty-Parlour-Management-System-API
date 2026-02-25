using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs
{
    public class AppointmentResponseDto
    {
        public int AppointmentId { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int StaffId { get; set; }
        public string StaffName { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string TimeSlot { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}