using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using SharedAlbumDto = PhotoAlbum.Shared.Models.SharedAlbum;
using SharedAlbumModel = PhotoAlbum.Data.Models.SharedAlbum;

namespace PhotoAlbum.Repository.Implementations
{
    public class SharedAlbumRepository : BaseRepository<SharedAlbumDto, SharedAlbumModel>, ISharedAlbumRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;


        public SharedAlbumRepository(ApplicationDbContext db, ILogger<SharedAlbumRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }
    }
}

