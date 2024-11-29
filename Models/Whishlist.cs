using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_app.Models
{
    public class Whishlist
    {
        [Key]
        public Guid Id { get; set; }

        public  Guid Userid { get; set; }

        public User User { get; set; }


        //[ForeignKey("Product")]

        public Guid Productid { get; set; }

        public Product Product {  get; set; }
    }
}
