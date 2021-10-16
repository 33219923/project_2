using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using SharedAlbumModel = PhotoAlbum.Data.Models.SharedAlbum;

namespace PhotoAlbum.Repository.Implementations
{
    public class SharedAlbumRepository : BaseRepository<SharedAlbumModel>, IPhotoRepository<SharedAlbumModel>
    {
        private readonly ApplicationDbContext _db;

        public SharedAlbumRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

