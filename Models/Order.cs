namespace Ecommerce_app.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal total_price { get; set; }
        public string orderstatus { get; set; }
        public DateTime placed_date { get; set; }
        public int quantity { get; set; }

        public Guid userid { get; set; }
        public User user { get; set; }

        public Guid productid { get; set; }
        public Product product { get; set; }

        public Guid addressid { get; set; }
        public Address address { get; set; }

       


    }
}
