using PhotoAlbum.Repository.Interfaces.Base;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface ISharedPhotoRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
