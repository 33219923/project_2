using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

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
    }
}

