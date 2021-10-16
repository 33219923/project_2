using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;

namespace PhotoAlbum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : BaseController<IAlbumService>
    {
        private readonly ILogger _logger;
        private readonly IAlbumService _albumService;

        public AlbumController(ILogger<AlbumController> logger, IAlbumService albumService) : base(logger, albumService)
        {
            _logger = logger;
            _albumService = albumService;
        }
    }
}
