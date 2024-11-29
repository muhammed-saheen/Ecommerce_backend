using Ecommerce_app.DTO;
using Ecommerce_app.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce_app.Controllers
{
    [Route("auth/")]
    [ApiController]
    public class Auth_controller : ControllerBase
    {
      private readonly IAuth_service service;
      public Auth_controller(IAuth_service service) { 
        this.service = service;
      }
        [HttpPost("login")]

        public async Task<IActionResult> login_([FromBody]Login_dto data)
        {
            var response =await service.Login(data);
            return StatusCode(response.Statuscode, response.Message);
        }

        [HttpPost("Signup")]

        public async Task<IActionResult> Register_([FromBody] Register_dto data)
        {
            var response = await service.Register_user(data);
            return StatusCode(response.Statuscode, response.Message);
        }

    }
}
