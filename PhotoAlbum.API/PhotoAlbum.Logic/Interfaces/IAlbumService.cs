using PhotoAlbum.Logic.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Logic.Interfaces
{
    public interface IAlbumService : IBaseService<Album>
    {
        SharedAlbum ShareAlbum(SharedAlbum sharedAlbum);
        void UnshareAlbum(SharedAlbum sharedAlbum);
        List<Album> ListAllShared();
        List<UserReference> ListAvailableUsers(Guid albumId);
        List<UserReference> ListSharedUsers(Guid albumId);
    }
}
