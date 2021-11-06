using PhotoAlbum.Logic.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Logic.Interfaces
{
    public interface IPhotoService : IBaseService<Photo>
    {
        SharedPhoto SharePhoto(SharedPhoto sharedPhoto);
        void UnsharePhoto(SharedPhoto sharedPhoto);
    }
}
