using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;

namespace PhotoAlbum.Repository.Implementations.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public virtual T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Guid Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> ListAll()
        {
            throw new NotImplementedException();
        }

        public virtual T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
