using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int quantity { get; set; }

        public Guid categoryid { get; set; }
        public Category category { get; set; }

        public ICollection<Cart_item> cartItem { get; set; }

        public ICollection<Whishlist> whishlists { get; set; }

    }
}
