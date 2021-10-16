using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhotoAlbum.Repository.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> ListAll(Func<T, bool> filter = null);
        T Get(Func<T, bool> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Func<T, bool> primaryKey);
    }
}
