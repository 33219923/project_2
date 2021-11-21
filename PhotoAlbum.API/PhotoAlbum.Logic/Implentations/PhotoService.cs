using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Models;
using PhotoAlbum.Shared.Services;
using System;
using System.Collections.Generic;
using PhotoDto = PhotoAlbum.Shared.Models.Photo;
using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Logic.Implentations
{
    public class PhotoService : BaseService<PhotoDto, PhotoModel>, IPhotoService
    {
        private readonly ISharedPhotoRepository _sharedPhotoRepository;
        private readonly ApplicationDbContext _db;

        public PhotoService(ILogger<PhotoService> logger,
            IPhotoRepository repository,
            ApplicationDbContext db,
            ISharedPhotoRepository sharedPhotoRepository,
            IRequestState requestState
            ) : base(logger, repository, requestState)
        {
            _db = db;
            _sharedPhotoRepository = sharedPhotoRepository;
        }

        public override PhotoDto Add(PhotoDto dto)
        {
            var result = base.Add(dto);
            _db.SaveChanges();
            return result;
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);
            _db.SaveChanges();
        }

        public SharedPhoto SharePhoto(SharedPhoto sharedPhoto)
        {
            var result = _sharedPhotoRepository.Add(sharedPhoto);
            _db.SaveChanges();
            return result;
        }

        public void UnsharePhoto(SharedPhoto sharedPhoto)
        {
            _sharedPhotoRepository.Delete(x => x.PhotoId == sharedPhoto.PhotoId && x.UserId == sharedPhoto.UserId);
            _db.SaveChanges();
        }

        public List<PhotoDto> ListAllShared()
        {
            var result = _sharedPhotoRepository.ListAllShared();
            return result;
        }

        public override PhotoDto Update(PhotoDto dto, Guid id)
        {
            var result = base.Update(dto, id);
            _db.SaveChanges();
            return result;
        }
    }
}
