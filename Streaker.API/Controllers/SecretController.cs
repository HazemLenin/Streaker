using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Streaker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("secret response!!!");
        }
    }
}
