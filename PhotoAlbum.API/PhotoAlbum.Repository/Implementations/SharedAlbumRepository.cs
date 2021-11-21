using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedAlbumDto = PhotoAlbum.Shared.Models.SharedAlbum;
using SharedAlbumModel = PhotoAlbum.Data.Models.SharedAlbum;

namespace PhotoAlbum.Repository.Implementations
{
    public class SharedAlbumRepository : BaseRepository<SharedAlbumDto, SharedAlbumModel>, ISharedAlbumRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly IRequestState _requestState;


        public SharedAlbumRepository(ApplicationDbContext db,
            ILogger<SharedAlbumRepository> logger,
            IMapper autoMapper,
            IRequestState requestState) : base(db, logger, autoMapper)
        {
            _db = db;
            _logger = logger;
            _autoMapper = autoMapper;
            _requestState = requestState;
        }

        public List<Shared.Models.Album> ListAllShared()
        {
            var list = _db.SharedAlbums.AsNoTracking()
                .Include(x => x.Album)
                .Where(x => x.UserId == _requestState.UserId)
                .Select(x => x.Album);

            return _autoMapper.Map<List<Shared.Models.Album>>(list.ToList());
        }

        public List<Shared.Models.UserReference> ListAvailableUsers(Guid albumId)
        {
            var existingSharedList = _db.SharedAlbums.AsNoTracking()
               .Where(x => x.AlbumId == albumId)
               .Select(x => x.UserId);

            var availableList = _db.Users.AsNoTracking().Where(x => !existingSharedList.Contains(x.Id) && x.Id != _requestState.UserId);

            return _autoMapper.Map<List<Shared.Models.UserReference>>(availableList.ToList()).OrderBy(x => x.Name).ToList();
        }

        public List<Shared.Models.UserReference> ListSharedUsers(Guid albumId)
        {
            var existingSharedList = _db.SharedAlbums.AsNoTracking()
               .Include(x => x.User)
               .Where(x => x.UserId != _requestState.UserId && x.AlbumId == albumId)
               .Select(x => x.User);

            return _autoMapper.Map<List<Shared.Models.UserReference>>(existingSharedList.ToList()).OrderBy(x=> x.Name).ToList();
        }
    }
}

