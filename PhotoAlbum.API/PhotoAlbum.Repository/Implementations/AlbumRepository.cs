using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Repository.Implementations
{
    public class AlbumRepository : BaseRepository<AlbumModel>, IAlbumRepository<AlbumModel>
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
