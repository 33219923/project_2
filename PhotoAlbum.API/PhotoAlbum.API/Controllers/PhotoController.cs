using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Shared.Models;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : BaseController<IPhotoService, Photo>
    {
        private readonly ILogger _logger;
        private readonly IPhotoService _photoService;

        public PhotoController(ILogger<PhotoController> logger, IPhotoService photoService) : base(logger, photoService)
        {
            _logger = logger;
            _photoService = photoService;
        }

        [HttpPost]
        [Route("share")]
        [Authorize]
        public ActionResult<SharedPhoto> ShareAlbum([FromBody] SharedPhoto sharedPhoto)
        {
            _logger.LogDebug("Album controller share photo called. SharedAlbum: {sharedPhoto}", sharedPhoto);
            var result = _photoService.SharePhoto(sharedPhoto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("unshare")]
        [Authorize]
        public ActionResult UnsharePhoto([FromBody] SharedPhoto sharedPhoto)
        {
            _logger.LogDebug("Album controller unshare photo called. SharedAlbum: {sharedPhoto}", sharedPhoto);
            _photoService.UnsharePhoto(sharedPhoto);
            return Ok();
        }

    }
}
