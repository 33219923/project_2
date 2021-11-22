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
using SharedPhotoDto = PhotoAlbum.Shared.Models.SharedPhoto;
using SharedPhotoModel = PhotoAlbum.Data.Models.SharedPhoto;

namespace PhotoAlbum.Repository.Implementations
{
    public class SharedPhotoRepository : BaseRepository<SharedPhotoDto, SharedPhotoModel>, ISharedPhotoRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly IRequestState _requestState;

        public SharedPhotoRepository(ApplicationDbContext db, ILogger<SharedPhotoRepository> logger,
            IRequestState requestState,
            IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
            _requestState = requestState;
        }

        public List<Shared.Models.Photo> ListAllShared(string searchString = null)
        {
            var list = _db.SharedPhotos.AsNoTracking()
                .Include(x => x.Photo)
                .ThenInclude(x=> x.Metadata)
                .Where(x => x.UserId == _requestState.UserId).ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(x => x.Photo.Metadata.Geolocation.Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Photo.Metadata.Tags.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return _autoMapper.Map<List<Shared.Models.Photo>>(list.Select(x => x.Photo).ToList());
        }

        public List<Shared.Models.UserReference> ListAvailableUsers(Guid photoId)
        {
            var existingSharedList = _db.SharedPhotos.AsNoTracking()
               .Where(x => x.PhotoId == photoId)
               .Select(x => x.UserId);

            var availableList = _db.Users.AsNoTracking().Where(x => !existingSharedList.Contains(x.Id) && x.Id != _requestState.UserId);

            return _autoMapper.Map<List<Shared.Models.UserReference>>(availableList.ToList()).OrderBy(x => x.Name).ToList();
        }

        public List<Shared.Models.UserReference> ListSharedUsers(Guid photoId)
        {
            var existingSharedList = _db.SharedPhotos.AsNoTracking()
               .Include(x => x.User)
               .Where(x => x.UserId != _requestState.UserId && x.PhotoId == photoId)
               .Select(x => x.User);

            return _autoMapper.Map<List<Shared.Models.UserReference>>(existingSharedList.ToList()).OrderBy(x => x.Name).ToList();
        }

        public Shared.Models.PhotoMetadata UpsertMetadata(Shared.Models.PhotoMetadata metadata)
        {
            var entity = _db.PhotoMetadatas.FirstOrDefault(x => x.PhotoId == metadata.PhotoId);

            if (null == entity)
            {
                entity = _autoMapper.Map<Data.Models.PhotoMetadata>(metadata);
                entity.PhotoId = metadata.PhotoId;
                _db.PhotoMetadatas.Add(entity);
            }
            else
            {
                entity = _autoMapper.Map(metadata, entity);
                _db.PhotoMetadatas.Update(entity);
            }

            return _autoMapper.Map<Shared.Models.PhotoMetadata>(entity);
        }
    }
}
