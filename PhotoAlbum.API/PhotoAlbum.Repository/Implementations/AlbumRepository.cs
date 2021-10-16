using AutoMapper;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using AlbumDto = PhotoAlbum.Shared.Models.Album;
using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Repository.Implementations
{
    public class AlbumRepository : BaseRepository<AlbumDto, AlbumModel>, IAlbumRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;

        public AlbumRepository(ApplicationDbContext db, ILogger<AlbumRepository> logger, IMapper autoMapper) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
        }
    }
}
