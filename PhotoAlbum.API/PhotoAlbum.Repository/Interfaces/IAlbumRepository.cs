using PhotoAlbum.Repository.Interfaces.Base;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IAlbumRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
