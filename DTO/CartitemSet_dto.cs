using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.DTO
{
    public class CartitemSet_dto
    {
        [Required]
        public int quantity { get; set; }
    }
}
