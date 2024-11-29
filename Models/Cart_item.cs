namespace Ecommerce_app.Models
{
    public class Cart_item
    {
        public Guid Id { get; set; }

        public Guid productid { get; set; }
        public Product product { get; set; }
        

        public Guid cart_id { get; set; }

        public Cart Cart { get; set; }

        public decimal price { get; set; }
        public int quantity { get; set; }


    }
}
