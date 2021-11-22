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
        private readonly IRequestState _requestState;
        private readonly IPhotoRepository _photoRepository;
        private readonly IBlobManager _blobManager;
        private readonly ApplicationDbContext _db;

        public PhotoService(ILogger<PhotoService> logger,
            IPhotoRepository repository,
            ApplicationDbContext db,
            ISharedPhotoRepository sharedPhotoRepository,
            IRequestState requestState,
            IBlobManager blobManager
            ) : base(logger, repository, requestState)
        {
            _db = db;
            _photoRepository = repository;
            _sharedPhotoRepository = sharedPhotoRepository;
            _blobManager = blobManager;
            _requestState = requestState;
        }

        public override PhotoDto Add(PhotoDto dto)
        {
            var result = base.Add(dto);

            //Upload to storage
            if (dto.Data != null && dto.Data.Length > 0)
                _blobManager.UploadPhoto(result.Id, dto.Data);

            _db.SaveChanges();

            return result;
        }

        public override PhotoDto Get(Guid id)
        {
            var result = base.Get(id);

            result.Data = _blobManager.DownloadPhoto(result.Id);

            return result;
        }

        public override List<PhotoDto> ListAll()
        {
            var result = base.ListAll();

            foreach (var photo in result)
            {
                photo.Data = _blobManager.DownloadPhoto(photo.Id);
            }

            return result;
        }

        public override void Delete(Guid id)
        {
            base.Delete(id);

            //Delete to storage
            _blobManager.DeletePhoto(id);

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

            foreach (var photo in result)
            {
                photo.Data = _blobManager.DownloadPhoto(photo.Id);
            }

            return result;
        }

        public override PhotoDto Update(PhotoDto dto, Guid id)
        {
            var result = base.Update(dto, id);

            //Upload to storage
            if (dto.Data != null && dto.Data.Length > 0)
                _blobManager.UploadPhoto(id, dto.Data);

            _db.SaveChanges();
            return result;
        }

        public List<UserReference> ListAvailableUsers(Guid photoId)
        {
            return _sharedPhotoRepository.ListAvailableUsers(photoId);
        }

        public List<UserReference> ListSharedUsers(Guid photoId)
        {
            return _sharedPhotoRepository.ListSharedUsers(photoId);
        }

        public PhotoMetadata UpsertMetadata(PhotoMetadata metadata)
        {
            var result = _sharedPhotoRepository.UpsertMetadata(metadata);
            _db.SaveChanges();
            return result;
        }

        public List<PhotoDto> GetPhotosForAlbum(Guid albumId)
        {
            var result = _photoRepository.ListAll(x => x.AlbumId == albumId);

            foreach (var photo in result)
            {
                photo.Data = _blobManager.DownloadPhoto(photo.Id);
            }

            return result;
        }

        public List<PhotoDto> GetAvailable()
        {
            var result = _photoRepository.ListAll(x => x.CreatedByUserId == _requestState.UserId && x.AlbumId.HasValue == false);

            foreach (var photo in result)
            {
                photo.Data = _blobManager.DownloadPhoto(photo.Id);
            }

            return result;
        }

        public void RelinkPhotos(PhotoRelink relink)
        {
            _photoRepository.RelinkPhotos(relink);
            _db.SaveChanges();
        }
    }
}
