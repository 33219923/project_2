using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces.Base;
using System;

namespace PhotoAlbum.API.Controllers.Base
{
    public abstract class BaseController<TService> : ControllerBase where TService : class, IBaseService
    {
        private readonly ILogger _logger;
        private readonly TService _service;

        protected BaseController(ILogger logger, TService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<string> Get([FromRoute] Guid id)
        {
            return Ok("AlbumController is working");
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<string> List()
        {
            return Ok("AlbumController is working");
        }

        [HttpPost]
        public ActionResult<string> Add()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public ActionResult<string> Update()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<string> Delete([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
