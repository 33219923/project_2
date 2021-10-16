using Microsoft.EntityFrameworkCore;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoAlbum.Repository.Implementations.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public T Get(Func<T, bool> filter)
        {
            return _db.Set<T>().AsNoTracking().FirstOrDefault(filter);
        }

        public List<T> ListAll(Func<T, bool> filter = null)
        {
            return _db.Set<T>().AsNoTracking().Where(filter).ToList();
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Delete(Func<T, bool> primaryKey)
        {
            var entitiesToRemove = _db.Set<T>().Where(primaryKey);
            _db.Set<T>().RemoveRange(entitiesToRemove);
        }
    }
}
