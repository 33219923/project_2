using PhotoAlbum.Repository.Interfaces.Base;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IUserRepository<T> : ISearchableRepository<T> where T : class
    {
    }
}
