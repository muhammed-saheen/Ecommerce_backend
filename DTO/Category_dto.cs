using System.ComponentModel.DataAnnotations;

namespace Ecommerce_app.DTO
{
    public class Category_dto
    {
        [Required (ErrorMessage ="category name is required")]
        public string name { get; set; }
    }
}
