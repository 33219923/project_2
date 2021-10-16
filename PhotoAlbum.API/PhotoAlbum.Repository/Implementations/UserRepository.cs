using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;

using UserModel = PhotoAlbum.Data.Models.ApplicationUser;

namespace PhotoAlbum.Repository.Implementations
{
    public class UserRepository : BaseRepository<UserModel>, IPhotoRepository<UserModel>
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}