using PhotoAlbum.Repository.Interfaces.Base;

using SharedPhotoDto = PhotoAlbum.Shared.Models.SharedPhoto;
using SharedPhotoModel = PhotoAlbum.Data.Models.SharedPhoto;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface ISharedPhotoRepository : IBaseRepository<SharedPhotoDto, SharedPhotoModel>
    {
    }
}
