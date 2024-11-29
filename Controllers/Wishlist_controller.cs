using AutoMapper;
using Ecommerce_app.Context;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce_app.Controllers
{
    [Route("api/wishlist")]
    [ApiController]
    public class Wishlist_controller : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Application_context context;
        private readonly Iwishlist service;
        public Wishlist_controller(Application_context context, IMapper mapper, Iwishlist service)
        {
            this.context = context;
            this.mapper = mapper;
            this.service = service;
        }
        [Authorize(Roles ="User")]
        [HttpPost("add/{productid}")]
        public async Task<IActionResult> Addtowishlist(Guid productid)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await service.Add_to_wishlist(productid, Guid.Parse(userid));
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete_wishliat(Guid productid)
        {
            var response = await service.Remove_wishlist(productid);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "User")]
        [HttpGet("get_wishlist")]
        public async Task<IActionResult> Get_wishlist()
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var response = await service.Get_item_wishlist(Guid.Parse(userid));
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    


    }

}
