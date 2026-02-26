using System.ComponentModel.DataAnnotations;
using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs.Staff
{
    public class StaffCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string StaffName { get; set; }

        [Required]
        public StaffRole Role { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Range(0, 50)]
        public int Experience { get; set; }

        [Range(0, double.MaxValue)]
        public double Salary { get; set; }
    }
}