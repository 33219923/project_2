using Microsoft.AspNetCore.Mvc;

namespace PhotoAlbum.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("UserController is working");
        }
    }
}
