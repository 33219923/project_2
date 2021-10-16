using PhotoAlbum.Repository.Interfaces.Base;

using PhotoDto = PhotoAlbum.Shared.Models.Photo;
using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<PhotoDto, PhotoModel>
    {
    }
}
