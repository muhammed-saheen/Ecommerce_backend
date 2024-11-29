namespace Ecommerce_app.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public int itemscount { get; set; }
        public decimal totalprice { get; set; }
        public  List<Cart_item> cart_Items { get; set; }
        public Guid Userid { get; set; }
        public User User { get; set; }

        

    }
}
