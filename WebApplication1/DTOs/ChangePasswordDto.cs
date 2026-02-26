using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(4)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "NewPassword and ConfirmPassword must match")]
        public string ConfirmPassword { get; set; }
    }
}