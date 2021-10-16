using PhotoAlbum.Repository.Interfaces.Base;

using SharedAlbumDto = PhotoAlbum.Shared.Models.SharedAlbum;
using SharedAlbumModel = PhotoAlbum.Data.Models.SharedAlbum;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface ISharedAlbumRepository : IBaseRepository<SharedAlbumDto, SharedAlbumModel>
    {
    }
}
