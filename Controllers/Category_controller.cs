using Ecommerce_app.Context;
using Ecommerce_app.DTO;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_app.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class Category_controller : ControllerBase
    {
        private readonly Application_context context;
        private readonly Icategory_service service;
        public Category_controller(Application_context context,Icategory_service service) {

            this.service = service;
          this.context = context;
        }

        [Authorize(Roles ="admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] Category_dto data)
        {
            var response =await service.Add_category(data);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{name}")]

        public async Task<IActionResult> Deletecategory(string name)
        {
            var response = await service.Remove_category(name);
            return StatusCode(response.Statuscode, response.Message);

        }

    }
}
