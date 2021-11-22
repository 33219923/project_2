using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
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


        public PhotoRepository(ApplicationDbContext db, ILogger<PhotoRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
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

