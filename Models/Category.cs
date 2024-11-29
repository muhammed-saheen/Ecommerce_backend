using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.Models
{
    public class Category
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }

        public ICollection<Product>  products { get; set; }
    }
}
