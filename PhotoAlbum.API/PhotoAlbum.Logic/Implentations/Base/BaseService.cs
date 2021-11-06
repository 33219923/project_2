using Microsoft.Extensions.Logging;
using PhotoAlbum.Data.Interfaces;
using PhotoAlbum.Logic.Interfaces.Base;
using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace PhotoAlbum.Logic.Implentations
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto> where TDto : class where TEntity : class
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<TDto, TEntity> _repository;

        public  BaseService(ILogger logger, IBaseRepository<TDto, TEntity> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public virtual TDto Add(TDto dto)
        {
            _logger.LogDebug("Base service add called! Dto:{dto}", dto);
            return _repository.Add(dto);
        }

        public virtual void Delete(Guid id)
        {
            _logger.LogDebug("Base service delete called! Id:{id}", id);
            _repository.Delete(id);
        }

        public virtual TDto Get(Guid id)
        {
            _logger.LogDebug("Base service get called! Id:{id}", id);
            return _repository.Get(x => ((IBaseEntity)x).Id == id);
        }

        public virtual List<TDto> ListAll()
        {
            _logger.LogDebug("Base service list all called!");
            return _repository.ListAll();
        }

        public virtual TDto Update(TDto dto, Guid id)
        {
            _logger.LogDebug("Base service update called! Dto:{dto}, Id:{id}", dto, id);
            return _repository.Update(dto, id);
        }
    }
}