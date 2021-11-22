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
    public class PhotoController : BaseController<IPhotoService, Photo>
    {
        private readonly ILogger _logger;
        private readonly IPhotoService _photoService;

        public PhotoController(ILogger<PhotoController> logger, IPhotoService photoService) : base(logger, photoService)
        {
            _logger = logger;
            _photoService = photoService;
        }

        [HttpGet]
        [Route("search")]
        [Authorize]
        public ActionResult<SharedPhoto> SearchPhoto([FromQuery] string searchString)
        {
            var result = _photoService.Search(searchString);
            return Ok(result);
        }

        [HttpPost]
        [Route("share")]
        [Authorize]
        public ActionResult<SharedPhoto> SharePhoto([FromBody] SharedPhoto sharedPhoto)
        {
            _logger.LogDebug("Photo controller share photo called. SharedPhoto: {sharedPhoto}", sharedPhoto);
            var result = _photoService.SharePhoto(sharedPhoto);
            return Ok(result);
        }

        [HttpPost]
        [Route("metadata")]
        [Authorize]
        public ActionResult<SharedPhoto> UpsertPhoto([FromBody] PhotoMetadata metaData)
        {
            _logger.LogDebug("Photo controller upsert photo data called. PhotoMetadata: {PhotoMetadata}", metaData);
            var result = _photoService.UpsertMetadata(metaData);
            return Ok(result);
        }

        [HttpDelete]
        [Route("unshare")]
        [Authorize]
        public ActionResult UnsharePhoto([FromBody] SharedPhoto sharedPhoto)
        {
            _logger.LogDebug("Photo controller unshare photo called. SharedPhoto: {sharedPhoto}", sharedPhoto);
            _photoService.UnsharePhoto(sharedPhoto);
            return Ok();
        }

        [HttpGet]
        [Route("list/shared")]
        [Authorize]
        public virtual ActionResult<List<Photo>> ListShared([FromQuery] string searchString)
        {
            var result = _photoService.ListAllShared(searchString);
            return Ok(result);
        }

        [HttpGet]
        [Route("list/shared/{photoId}/available")]
        [Authorize]
        public virtual ActionResult<List<UserReference>> ListAvailableUsers([FromRoute] Guid photoId)
        {
            var result = _photoService.ListAvailableUsers(photoId);
            return Ok(result);
        }

        [HttpGet]
        [Route("list/shared/{photoId}")]
        [Authorize]
        public virtual ActionResult<List<UserReference>> ListSharedUsers([FromRoute] Guid photoId)
        {
            var result = _photoService.ListSharedUsers(photoId);
            return Ok(result);
        }

        [HttpGet]
        [Route("album/{albumId}")]
        [Authorize]
        public ActionResult<List<Photo>> GetPhotosForAlbum([FromRoute] Guid albumId)
        {
            var result = _photoService.GetPhotosForAlbum(albumId);
            return Ok(result);
        }

        [HttpGet]
        [Route("available")]
        [Authorize]
        public ActionResult<List<Photo>> GetAvailablePhotos()
        {
            var result = _photoService.GetAvailable();
            return Ok(result);
        }

        [HttpPost]
        [Route("relink")]
        [Authorize]
        public ActionResult UpsertPhoto([FromBody] PhotoRelink relink)
        {
            _photoService.RelinkPhotos(relink);
            return Ok();
        }

    }
}
