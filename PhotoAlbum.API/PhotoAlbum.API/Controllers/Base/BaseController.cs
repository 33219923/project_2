using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace PhotoAlbum.API.Controllers.Base
{
    public abstract class BaseController<TService, TDto> : ControllerBase where TService : class, IBaseService<TDto>
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
        public ActionResult<TDto> Get([FromRoute] Guid id)
        {
            _logger.LogDebug("Base controller get called. Id:{id}", id);
            var result = _service.Get(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("list")]
        public ActionResult<List<TDto>> List()
        {
            _logger.LogDebug("Base controller list called.");
            var result = _service.ListAll();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<TDto> Add([FromBody] TDto dto)
        {
            _logger.LogDebug("Base controller add called. Dto: {dto}", dto);
            var result = _service.Add(dto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<TDto> Update([FromRoute] Guid id, [FromBody] TDto dto)
        {
            _logger.LogDebug("Base controller update called. Id:{}, Dto: {dto}", id, dto);
            var result = _service.Update(dto, id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            _logger.LogDebug("Base controller delete called. Id:{}", id);
            _service.Delete(id);
            return Ok();
        }
    }
}
