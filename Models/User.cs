using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public  string username { get; set; }

        public string Role { get; set; } = "User";

        public bool Status { get; set; } = true;

        public Cart cart { get; set; }

        public ICollection<Order> orders { get; set; }

        public ICollection<Address> address { get; set; }
    }
}
