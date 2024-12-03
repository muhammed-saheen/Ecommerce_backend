using Ecommerce_app.Models;

namespace Ecommerce_app.DTO
{
    public class Cart_view
    {
        public Guid Id { get; set; }

        public int itemscount { get; set; }
        public decimal totalprice { get; set; }
      
        public ICollection<Cart_itemView_dto> cart_Items { get; set; }

    }
}
