using PhotoAlbum.Repository.Interfaces.Base;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IPhotoMetadataRepository<T> : ISearchableRepository<T> where T : class
    {
    }
}
