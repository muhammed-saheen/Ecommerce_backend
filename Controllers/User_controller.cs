using AutoMapper;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Ecommerce_app.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class User_controller : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly Iuser_service service;
        public User_controller(IMapper mapper,Iuser_service service) {
         this.mapper = mapper;
         this.service = service;
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<IActionResult> GetallUser() { 
        var response=await service.Get_all_users();
        return Ok(response);
        }
        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await service.Delete_user(id);
            return StatusCode(response.Statuscode,response.Message);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("block/{id}")]
        public async Task<IActionResult> Bolockuser(Guid id)
        {
            var response = await service.Block_user(id);
            return StatusCode(response.Statuscode, response.Message);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Getuser(Guid id)
        {
            var response = await service.Getuser_byid(id);
            if (response==null)
            {
                return NotFound();
            }
            return Ok(response);
        }

    }
}
