namespace Ecommerce_app.DTO
{
    public class Cart_itemView_dto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal price { get; set; }
        public int quantity { get; set; }

    }
}
