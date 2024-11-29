using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.DTO
{
    public class Login_dto
    {
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "username is required.")]
        [StringLength(50, ErrorMessage = "username cannot exceed 50 characters.")]
        public string Username { get; set; }
    }
}
