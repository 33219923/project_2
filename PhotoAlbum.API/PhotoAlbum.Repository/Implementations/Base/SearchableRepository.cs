using Microsoft.EntityFrameworkCore;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoAlbum.Repository.Implementations.Base
{
    public class SearchableRepository<T> : BaseRepository<T>, ISearchableRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public SearchableRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public virtual List<T> Search(string searchPhrase, Func<T, bool> filter = null)
        {
            IEnumerable<T> dbSet = _db.Set<T>().AsNoTracking();

            if (null != filter)
                dbSet = dbSet.Where(filter);

            return dbSet.Where(p => ((ISearchable)p).SearchVector.Matches(searchPhrase)).ToList();
        }
    }
}
