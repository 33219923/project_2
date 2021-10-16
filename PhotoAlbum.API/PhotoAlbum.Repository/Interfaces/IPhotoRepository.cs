using PhotoAlbum.Repository.Interfaces.Base;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IPhotoRepository<T> : IBaseRepository<T> where T : class
    {
    }
}
