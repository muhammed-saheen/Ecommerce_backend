using Ecommerce_app.DTO;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce_app.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class Order_controller : ControllerBase
    {
        private readonly IOrder_service service;
        public Order_controller(IOrder_service service) { 
        this.service = service;
        }

        [Authorize(Roles ="User")]
        [HttpGet("placeOrder")]
        public async Task<IActionResult> Place_order(Guid productid,int quantity,Guid addressid) {
           
            var userid=User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value;
            if (userid == null) { 
            return Unauthorized();
            }
            var response = await service.Place_order(Guid.Parse(userid),productid,quantity,addressid);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{orderid}")]
        public async Task<IActionResult> UpdateOrder(Guid orderid, string status )
        {
            var response = await service.update_order(orderid,status);
            return StatusCode(response.Statuscode, response.Message);


        }

        [Authorize(Roles = "User")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteOrder(Guid orderid)
        {
            var response = await service.delete_order(orderid);
            return StatusCode(response.Statuscode, response.Message);

        }
        [Authorize(Roles ="admin")]
        [HttpGet("allorder")]
        public async Task<IActionResult> Getallorder()
        {
            var response = await service.getallorder();
            return Ok(response);

        }

        [Authorize(Roles = "User")]
        [HttpGet("orderbyUser")]
        public async Task<IActionResult> Orderbyuser(Guid userid)
        {
            var response = await service.oderby_userid(userid);
            return Ok(response);

        }
        [Authorize(Roles = "User")]
        [HttpPost("add-address")]
        public async Task<IActionResult> Addaddress([FromBody] Address_set_dto data)
        {
            var response=await service.AddAddress(data);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("delete-address")]
        public async Task<IActionResult> Deleteaddress(Guid id)
        {
            var response = await service.RemoveAddress(id);
            return StatusCode(response.Statuscode, response.Message);
        }

    }
}
