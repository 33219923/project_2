using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Shared.Models;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : BaseController<IAlbumService, Album>
    {
        private readonly ILogger _logger;
        private readonly IAlbumService _albumService;

        public AlbumController(ILogger<AlbumController> logger, IAlbumService albumService) : base(logger, albumService)
        {
            _logger = logger;
            _albumService = albumService;
        }

        [HttpPost]
        [Route("share")]
        public ActionResult<SharedAlbum> ShareAlbum([FromBody] SharedAlbum sharedAlbum)
        {
            _logger.LogDebug("Album controller share album called. SharedAlbum: {sharedAlbum}", sharedAlbum);
            var result = _albumService.ShareAlbum(sharedAlbum);
            return Ok(result);
        }

        [HttpDelete]
        [Route("unshare")]
        public ActionResult UnshareAlbum([FromBody] SharedAlbum sharedAlbum)
        {
            _logger.LogDebug("Album controller unshare album called. SharedAlbum: {sharedAlbum}", sharedAlbum);
            _albumService.UnshareAlbum(sharedAlbum);
            return Ok();
        }

    }
}
