using Ecommerce_app.DTO;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce_app.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class Cart_Controller : ControllerBase
    {
        private readonly ICartitem_service service;
        public Cart_Controller(ICartitem_service service) {
            this.service = service;
        }

        [Authorize(Roles = "User")]
        [HttpPost("add/{productid}")]

        public async Task<IActionResult> Additem(Guid productid, [FromBody] CartitemSet_dto data)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = await service.add_cartitem(productid, Guid.Parse(userid), data.quantity);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{itemid}")]
        public async Task<IActionResult> Deleteitem(Guid itemid)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = await service.delete_cartitem(itemid,Guid.Parse(userid));
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "User")]
        [HttpPut("{itemid}")]
        public async Task<IActionResult> Updateitem(Guid itemid, [FromBody] CartitemSet_dto data)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = await service.update_cartitem(itemid,data,Guid.Parse(userid));
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> getitem()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = await service.get_cartitem(Guid.Parse(userid));
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [Authorize(Roles = "User")]
        [HttpGet("ckeckout")]
        public async Task<IActionResult> Checkout(Guid addressid)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var response = await service.Checkout(Guid.Parse(userid), addressid);
            return StatusCode(response.Statuscode, response.Message);

        }
    }
}
