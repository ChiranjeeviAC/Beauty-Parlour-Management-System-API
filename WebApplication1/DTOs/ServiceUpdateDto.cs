using System.ComponentModel.DataAnnotations;
using WebApplication1.Model.Enums;

namespace WebApplication1.DTOs
{
    public class ServiceUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string ServiceName { get; set; }

        [Required]
        public ServiceCategory Category { get; set; }

        [Range(1, 100000)]
        public double Price { get; set; }

        [Range(1, 600)]
        public int Duration { get; set; }
    }
}