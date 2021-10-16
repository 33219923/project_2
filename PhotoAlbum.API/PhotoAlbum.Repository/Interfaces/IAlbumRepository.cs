using PhotoAlbum.Repository.Interfaces.Base;

using AlbumDto = PhotoAlbum.Shared.Models.Album;
using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IAlbumRepository : IBaseRepository<AlbumDto, AlbumModel>
    {
    }
}
