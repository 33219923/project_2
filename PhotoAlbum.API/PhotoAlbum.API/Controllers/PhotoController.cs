using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : BaseController<IPhotoService>
    {
        private readonly ILogger _logger;
        private readonly IPhotoService _photoService;

        public PhotoController(ILogger<PhotoController> logger, IPhotoService photoService) : base(logger, photoService)
        {
            _logger = logger;
            _photoService = photoService;
        }

    }
}
