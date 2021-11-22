using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using PhotoDto = PhotoAlbum.Shared.Models.Photo;
using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Repository.Implementations
{
    public class PhotoRepository : BaseRepository<PhotoDto, PhotoModel>, IPhotoRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly IRequestState _requestState;


        public PhotoRepository(ApplicationDbContext db, ILogger<PhotoRepository> logger, IRequestState requestState, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
            _requestState = requestState;
        }

        public override PhotoDto Get(Func<PhotoModel, bool> filter)
        {
            var entity = _db.Photos.Include(x => x.Metadata).AsNoTracking().FirstOrDefault(filter);

            if (null != entity)
                return _autoMapper.Map<PhotoDto>(entity);

            return null;
        }

        public override List<PhotoDto> ListAll(Func<PhotoModel, bool> filter = null)
        {
            IEnumerable<PhotoModel> query = _db.Set<PhotoModel>().AsNoTracking().Include(x => x.Metadata);

            if (filter != null) query = query.Where(filter);

            return _autoMapper.Map<List<PhotoDto>>(query.ToList());
        }

        public List<PhotoDto> ListAll(string searchString = null)
        {
            var list = _db.Photos.AsNoTracking()
                .Include(x => x.Metadata)
                .Where(x => x.CreatedByUserId == _requestState.UserId)
                .ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Metadata.Geolocation.Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Metadata.Tags.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return _autoMapper.Map<List<PhotoDto>>(list);
        }

        public void RelinkPhotos(Shared.Models.PhotoRelink relink)
        {
            var existingPhotos = _db.Photos.Where(x => x.AlbumId == relink.AlbumId);

            var removed = existingPhotos.Where(x => !relink.PhotoIds.Contains(x.Id));
            foreach (var r in removed)
                r.AlbumId = null;

            var added = _db.Photos.Where(x => relink.PhotoIds.Contains(x.Id) && x.AlbumId == null);
            foreach (var a in added)
                a.AlbumId = relink.AlbumId;

            if (removed.Any())
                _db.Photos.UpdateRange(removed);
            if (removed.Any())
                _db.Photos.UpdateRange(added);
        }
    }
}

