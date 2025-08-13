using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Hello World");
        }

        [HttpGet("greeting")]
        public ActionResult<object> GetGreeting([FromQuery] string name = "World")
        {
            return Ok(new { message = $"Hello {name}!" });
        }
    }
}