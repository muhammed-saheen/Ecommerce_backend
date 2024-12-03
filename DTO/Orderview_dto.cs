using Ecommerce_app.Models;

namespace Ecommerce_app.DTO
{
    public class Orderview_dto
    {
        public Guid Id { get; set; }
        public decimal total_price { get; set; }
        public string orderstatus { get; set; }
        public DateTime placed_date { get; set; }
        public int quantity { get; set; }
        public string productname { get; set; }
        public Address address { get; set; }
    }
}
