using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Data.Interfaces;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using PhotoMetadataDto = PhotoAlbum.Shared.Models.PhotoMetadata;
using PhotoMetadataModel = PhotoAlbum.Data.Models.PhotoMetadata;

namespace PhotoAlbum.Repository.Implementations
{
    public class PhotoMetadataRepository : SearchableRepository<PhotoMetadataDto, PhotoMetadataModel>, IPhotoMetadataRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;


        public PhotoMetadataRepository(ApplicationDbContext db, ILogger<PhotoMetadataRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }

        public override List<PhotoMetadataDto> Search(string searchPhrase, Func<PhotoMetadataModel, bool> filter = null)
        {
            IEnumerable<PhotoMetadataModel> dbSet = _db.Set<PhotoMetadataModel>().AsNoTracking();

            if (null != filter)
                dbSet = dbSet.Where(filter);

            var entities = dbSet.Where(p => ((ISearchable)p).SearchVector.Matches(searchPhrase)).ToList();

            return _autoMapper.Map<List<PhotoMetadataDto>>(entities);
        }
    }
}
