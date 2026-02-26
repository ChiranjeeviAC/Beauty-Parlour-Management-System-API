using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class UserS
    {
        [Key]
        public int UserSId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // Foreign Key
        public int StaffId { get; set; }

        [ForeignKey("StaffId")]
        public Staff Staff { get; set; }
    }
}