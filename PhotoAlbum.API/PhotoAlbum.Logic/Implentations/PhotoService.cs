using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using System;
using PhotoDto = PhotoAlbum.Shared.Models.Photo;
using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Logic.Implentations
{
    public class PhotoService : BaseService<PhotoDto, PhotoModel>, IPhotoService
    {
        private readonly ISharedPhotoRepository _sharedPhotoRepository;

        public PhotoService(ILogger<PhotoService> logger, IPhotoRepository repository, ISharedPhotoRepository sharedPhotoRepository) : base(logger, repository)
        {
            _sharedPhotoRepository = sharedPhotoRepository;
        }

        public SharedPhoto SharePhoto(SharedPhoto sharedPhoto)
        {
            return _sharedPhotoRepository.Add(sharedPhoto);
        }

        public void UnsharePhoto(SharedPhoto sharedPhoto)
        {
            _sharedPhotoRepository.Delete(x => x.PhotoId == sharedPhoto.PhotoId && x.UserId == sharedPhoto.UserId);
        }
    }
}
