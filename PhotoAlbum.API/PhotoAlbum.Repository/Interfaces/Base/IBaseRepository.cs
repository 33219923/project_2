using System;
using System.Collections.Generic;

namespace PhotoAlbum.Repository.Interfaces.Base
{
    public interface IBaseRepository<TDto, TEntity> where TDto : class where TEntity : class
    {
        List<TDto> ListAll(Func<TEntity, bool> filter = null);
        TDto Get(Func<TEntity, bool> filter);
        void Add(TDto entity);
        void Update(TDto entity, Guid id);
        void Update(TDto dto, Func<TEntity, bool> primaryKey);
        void Delete(Guid id);
        void Delete(Func<TEntity, bool> primaryKey);
    }
}
