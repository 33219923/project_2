using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using SharedPhotoDto = PhotoAlbum.Shared.Models.SharedPhoto;
using SharedPhotoModel = PhotoAlbum.Data.Models.SharedPhoto;

namespace PhotoAlbum.Repository.Implementations
{
    public class SharedPhotoRepository : BaseRepository<SharedPhotoDto, SharedPhotoModel>, ISharedPhotoRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public SharedPhotoRepository(ApplicationDbContext db, ILogger<SharedPhotoRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }
    }
}
