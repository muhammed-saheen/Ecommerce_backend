using Ecommerce_app.DTO;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Ecommerce_app.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class Product_controller : ControllerBase
    {
        private readonly IProduct_service service;
        public Product_controller(IProduct_service service) {
            this.service = service;
        }
        [HttpGet("all_product")]
        public async Task<IActionResult> GetAllproduct()
        {
            var response = await service.Get_allProduct();
            if (response == null) {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Productbyid(Guid id)
        {
            var response = service.Get_product_by_id(id);
            return Ok(response);
        }

        [HttpGet("{category}")]
        public async Task<IActionResult> ProductbyCatgory(string category)
        {
            var response = service.get_product_by_category(category);
            return Ok(response);
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> ProductPaginated(int pageNumber,int pagesize)
        {
            var response =await service.Paginated_products(pageNumber,pagesize);
            return Ok(response);
        }

        [Authorize(Roles ="admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromBody] Productset_dto data)
        {
            var response =await service.Update_product(data);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await service.Delete_product(id);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("add_product")]
        public async Task<IActionResult> Addproduct([FromBody] Product_dto data)
        {
            var response = await service.Add_new_product(data);
            return StatusCode(response.Statuscode, response.Message);
        }


    }
}
