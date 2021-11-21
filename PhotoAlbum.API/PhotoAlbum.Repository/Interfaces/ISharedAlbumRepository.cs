using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using SharedAlbumDto = PhotoAlbum.Shared.Models.SharedAlbum;
using SharedAlbumModel = PhotoAlbum.Data.Models.SharedAlbum;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface ISharedAlbumRepository : IBaseRepository<SharedAlbumDto, SharedAlbumModel>
    {
        List<Shared.Models.Album> ListAllShared();
        List<Shared.Models.UserReference> ListAvailableUsers(Guid albumId);
        List<Shared.Models.UserReference> ListSharedUsers(Guid albumId);
    }
}
