using PhotoAlbum.Repository.Interfaces.Base;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface ISharedAlbumRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
