using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.DTO
{
    public class User_dto
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string username { get; set; }

        public string Role { get; set; } = "User";

        public bool Status { get; set; } = true;
    }
}
