using Microsoft.AspNetCore.Mvc;

namespace PhotoAlbum.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("PhotoController is working");
        }
    }
}
