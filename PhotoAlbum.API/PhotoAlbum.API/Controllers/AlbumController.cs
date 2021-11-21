using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.API.Controllers.Base;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Shared.Models;
using System;
using System.Collections.Generic;

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
        [Authorize]
        public ActionResult<SharedAlbum> ShareAlbum([FromBody] SharedAlbum sharedAlbum)
        {
            _logger.LogDebug("Album controller share album called. SharedAlbum: {sharedAlbum}", sharedAlbum);
            var result = _albumService.ShareAlbum(sharedAlbum);
            return Ok(result);
        }

        [HttpDelete]
        [Route("unshare")]
        [Authorize]
        public ActionResult UnshareAlbum([FromBody] SharedAlbum sharedAlbum)
        {
            _logger.LogDebug("Album controller unshare album called. SharedAlbum: {sharedAlbum}", sharedAlbum);
            _albumService.UnshareAlbum(sharedAlbum);
            return Ok();
        }

        [HttpGet]
        [Route("list/shared")]
        [Authorize]
        public virtual ActionResult<List<Album>> ListShared()
        {
            var result = _albumService.ListAllShared();
            return Ok(result);
        }

        [HttpGet]
        [Route("list/shared/{albumId}/available")]
        [Authorize]
        public virtual ActionResult<List<UserReference>> ListAvailableUsers([FromRoute] Guid albumId)
        {
            var result = _albumService.ListAvailableUsers(albumId);
            return Ok(result);
        }

        [HttpGet]
        [Route("list/shared/{albumId}")]
        [Authorize]
        public virtual ActionResult<List<UserReference>> ListSharedUsers([FromRoute] Guid albumId)
        {
            var result = _albumService.ListSharedUsers(albumId);
            return Ok(result);
        }

    }
}
