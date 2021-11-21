using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Services;
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

        public List<Shared.Models.Photo> ListAllShared()
        {
            var list = _db.SharedPhotos.AsNoTracking()
                .Include(x => x.Photo)
                .Where(x => x.UserId == _requestState.UserId)
                .Select(x => x.Photo);

            return _autoMapper.Map<List<Shared.Models.Photo>>(list.ToList());
        }
    }
}
