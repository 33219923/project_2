using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using PhotoAlbum.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using AlbumDto = PhotoAlbum.Shared.Models.Album;
using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Logic.Implentations
{
    public class AlbumService : BaseService<AlbumDto, AlbumModel>, IAlbumService
    {
        private readonly ISharedAlbumRepository _sharedAlbumRepository;
        private readonly IBlobManager _blobManager;
        private readonly IPhotoRepository _photoRepository;
        private readonly ApplicationDbContext _db;

        public AlbumService(ILogger<AlbumService> logger,
            IAlbumRepository repository,
            IPhotoRepository photoRepository,
            ApplicationDbContext db,
            ISharedAlbumRepository sharedAlbumRepository,
            IRequestState requestState,
            IBlobManager blobManager
            ) : base(logger, repository, requestState)
        {
            _sharedAlbumRepository = sharedAlbumRepository;
            _db = db;
            _photoRepository = photoRepository;
            _blobManager = blobManager;
        }

        public override AlbumDto Add(AlbumDto dto)
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



        public SharedAlbum ShareAlbum(SharedAlbum sharedAlbum)
        {
            var result = _sharedAlbumRepository.Add(sharedAlbum);
            _db.SaveChanges();
            return result;
        }

        public void UnshareAlbum(SharedAlbum sharedAlbum)
        {
            _sharedAlbumRepository.Delete(x => x.AlbumId == sharedAlbum.AlbumId && x.UserId == sharedAlbum.UserId);
            _db.SaveChanges();
        }

        public List<Album> ListAllShared()
        {
            var result = _sharedAlbumRepository.ListAllShared();

            foreach (var album in result)
            {
                var previewPhoto = _photoRepository.ListAll(x => x.AlbumId == album.Id).FirstOrDefault();

                if (previewPhoto != null)
                {
                    album.PreviewData = _blobManager.DownloadPhoto(previewPhoto.Id);
                    album.PreviewFilename = previewPhoto.Filename;
                }
            }

            return result;
        }

        public override AlbumDto Update(AlbumDto dto, Guid id)
        {
            var result = base.Update(dto, id);
            _db.SaveChanges();
            return result;
        }

        public List<UserReference> ListAvailableUsers(Guid albumId)
        {
            return _sharedAlbumRepository.ListAvailableUsers(albumId);
        }

        public List<UserReference> ListSharedUsers(Guid albumId)
        {
            return _sharedAlbumRepository.ListSharedUsers(albumId);
        }

        public override AlbumDto Get(Guid id)
        {
            var result = base.Get(id);

            var previewPhoto = _photoRepository.ListAll(x => x.AlbumId == result.Id).FirstOrDefault();

            if (previewPhoto != null)
            {
                result.PreviewData = _blobManager.DownloadPhoto(previewPhoto.Id);
                result.PreviewFilename = previewPhoto.Filename;
            }

            return result;
        }

        public override List<AlbumDto> ListAll()
        {
            var result = base.ListAll();

            foreach (var album in result)
            {
                var previewPhoto = _photoRepository.ListAll(x => x.AlbumId == album.Id).FirstOrDefault();

                if (previewPhoto != null)
                {
                    album.PreviewData = _blobManager.DownloadPhoto(previewPhoto.Id);
                    album.PreviewFilename = previewPhoto.Filename;
                }
            }

            return result;
        }
    }
}
