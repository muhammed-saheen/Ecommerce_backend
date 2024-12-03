namespace Ecommerce_app.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string state { get; set; }
        public string City { get; set; }
        public string street { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        public Guid userid { get; set; }
        public User User { get; set; }

    }
}
