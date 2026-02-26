using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs.Service
{
    public class ServiceResponseDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public ServiceCategory Category { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
    }
}