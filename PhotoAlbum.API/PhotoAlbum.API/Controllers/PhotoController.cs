using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces;

namespace PhotoAlbum.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPhotoService _photoService;

        public PhotoController(ILogger<PhotoController> logger, IPhotoService photoService)
        {
            _logger = logger;
            _photoService = photoService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("PhotoController is working");
        }
    }
}
