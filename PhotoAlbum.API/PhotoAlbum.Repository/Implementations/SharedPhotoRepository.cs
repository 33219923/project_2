using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using SharedPhotoModel = PhotoAlbum.Data.Models.SharedPhoto;

namespace PhotoAlbum.Repository.Implementations
{
    public class SharedPhotoRepository : BaseRepository<SharedPhotoModel>, IPhotoRepository<SharedPhotoModel>
    {
        private readonly ApplicationDbContext _db;

        public SharedPhotoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
