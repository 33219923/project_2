using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using PhotoAlbum.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoAlbum.Repository.Implementations.Base
{
    public class BaseRepository<TDto, TEntity> : IBaseRepository<TDto, TEntity> where TDto : class, new() where TEntity : class, new()
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public BaseRepository(ApplicationDbContext db, ILogger logger, IMapper autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }

        public virtual TDto Get(Func<TEntity, bool> filter)
        {
            var entity = _db.Set<TEntity>().AsNoTracking().FirstOrDefault(filter);

            if (null != entity)
                return _autoMapper.Map<TDto>(entity);

            return null;
        }

        public virtual List<TDto> ListAll(Func<TEntity, bool> filter = null)
        {
            var entities = _db.Set<TEntity>().AsNoTracking().Where(filter).ToList();

            if (null != entities)
                return _autoMapper.Map<List<TDto>>(entities);

            return null;
        }

        public virtual void Add(TDto dto)
        {
            var entity = _autoMapper.Map<TEntity>(dto);

            _db.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TDto dto, Guid id)
        {
            var entity = _db.Set<TEntity>().FirstOrDefault(p => ((IBaseEntity)p).Id == id);

            if (null == entity) throw new EntityNotFoundException($"The entity with id [{id}] does not exist and could not be updated.");

            entity = _autoMapper.Map(dto, entity);

            _db.Set<TEntity>().Update(entity);
        }

        public virtual void Update(TDto dto, Func<TEntity, bool> primaryKey)
        {
            var entity = _db.Set<TEntity>().FirstOrDefault(primaryKey);

            if (null == entity) throw new EntityNotFoundException($"The entity with primary key [{primaryKey}] does not exist and could not be updated.");

            entity = _autoMapper.Map(dto, entity);

            _db.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entityToRemove = _db.Set<TEntity>().FirstOrDefault(p => ((IBaseEntity)p).Id == id);

            if (null == entityToRemove) throw new EntityNotFoundException($"The entity with id [{id}] does not exist and could not be deleted.");

            _db.Set<TEntity>().Remove(entityToRemove);
        }

        public virtual void Delete(Func<TEntity, bool> primaryKey)
        {
            var entitiesToRemove = _db.Set<TEntity>().Where(primaryKey);

            if (!entitiesToRemove.Any()) throw new EntityNotFoundException($"The entity with primary key [{primaryKey}] does not exist and could not be deleted.");

            _db.Set<TEntity>().RemoveRange(entitiesToRemove);
        }
    }
}
