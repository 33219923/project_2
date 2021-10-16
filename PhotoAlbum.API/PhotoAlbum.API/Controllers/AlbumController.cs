using Microsoft.AspNetCore.Mvc;

namespace PhotoAlbum.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("AlbumController is working");
        }
    }
}
