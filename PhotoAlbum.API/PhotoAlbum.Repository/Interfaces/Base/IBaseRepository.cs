using System;
using System.Collections.Generic;

namespace PhotoAlbum.Repository.Interfaces.Base
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> ListAll();
        T GetById(Guid id);
        T Add(T entity);
        T Update(T entity);
        Guid Delete(T entity);
    }
}
