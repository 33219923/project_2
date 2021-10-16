using System;
using System.Collections.Generic;

namespace PhotoAlbum.Repository.Interfaces.Base
{
    public interface ISearchableRepository<TDto, TEntity> : IBaseRepository<TDto, TEntity> where TDto : class where TEntity : class
    {
        List<TDto> Search(string searchPhrase, Func<TEntity, bool> filter = null);
    }
}
