using Microsoft.Extensions.Logging;
using PhotoAlbum.Data;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using System;
using System.Collections.Generic;
using AlbumDto = PhotoAlbum.Shared.Models.Album;
using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Logic.Implentations
{
    public class AlbumService : BaseService<AlbumDto, AlbumModel>, IAlbumService
    {
        private readonly ISharedAlbumRepository _sharedAlbumRepository;
        private readonly ApplicationDbContext _db;

        public AlbumService(ILogger<AlbumService> logger, 
            IAlbumRepository repository, 
            ApplicationDbContext db,
            ISharedAlbumRepository sharedAlbumRepository) : base(logger, repository)
        {
            _sharedAlbumRepository = sharedAlbumRepository;
            _db = db;
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

        public override AlbumDto Update(AlbumDto dto, Guid id)
        {
            var result = base.Update(dto, id);
            _db.SaveChanges();
            return result;
        }
    }
}
