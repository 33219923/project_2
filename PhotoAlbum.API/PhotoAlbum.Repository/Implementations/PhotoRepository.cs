using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Repository.Implementations
{
    public class PhotoRepository : BaseRepository<PhotoModel>, IPhotoRepository<PhotoModel>
    {
        private readonly ApplicationDbContext _db;

        public PhotoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

