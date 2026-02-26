using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Customer
{
    public class CustomerCreateDto
    {
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
    }
}