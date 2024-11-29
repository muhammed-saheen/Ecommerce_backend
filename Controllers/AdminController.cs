using Ecommerce_app.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_app.Controllers
{
    [Route("api/dasboard")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IAdmin_service service;

        public AdminController(IAdmin_service service)
        {
            this.service = service;
        }

        [Authorize(Roles ="admin")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> AdminDashboard()
        {
            var response =await service.Admin_dashboard();
            return Ok(response);
        }
    }
}
