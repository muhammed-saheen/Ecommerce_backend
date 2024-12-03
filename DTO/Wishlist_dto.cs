using Ecommerce_app.Models;

namespace Ecommerce_app.DTO
{
    public class Wishlist_dto
    {
        public Guid Id { get; set; }
        public Guid Productid { get; set; }

        public string productname { get; set; }

        public string productprice { get; set; }

    }
}
