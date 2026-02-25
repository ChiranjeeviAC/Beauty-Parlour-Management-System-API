using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs
{
    public class StaffResponseDto
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public StaffRole Role { get; set; }
        public string Phone { get; set; }
        public int Experience { get; set; }
        public double Salary { get; set; }
    }
}