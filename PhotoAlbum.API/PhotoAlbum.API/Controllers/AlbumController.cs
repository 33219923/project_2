using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces;

namespace PhotoAlbum.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAlbumService _albumService;

        public AlbumController(ILogger<AlbumController> logger, IAlbumService albumService)
        {
            _logger = logger;
            _albumService = albumService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("AlbumController is working");
        }
    }
}
