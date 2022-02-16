using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDesafioCoodesh.Controllers.V1
{
    [Route("")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok("Back-end Challenge 2021 🏅 - Space Flight News");
        }
    }
}
