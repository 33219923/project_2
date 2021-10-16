using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoAlbum.Repository.Implementations.Base
{
    public class SearchableRepository<TDto, TEntity> : BaseRepository<TDto, TEntity>, ISearchableRepository<TDto, TEntity> where TDto : class, new() where TEntity : class, new()
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public SearchableRepository(ApplicationDbContext db, ILogger logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }

        public virtual List<TDto> Search(string searchPhrase, Func<TEntity, bool> filter = null)
        {
            IEnumerable<TEntity> dbSet = _db.Set<TEntity>().AsNoTracking();

            if (null != filter)
                dbSet = dbSet.Where(filter);

            var entities = dbSet.Where(p => ((ISearchable)p).SearchVector.Matches(searchPhrase)).ToList();

            return _autoMapper.Map<List<TDto>>(entities);
        }
    }
}
