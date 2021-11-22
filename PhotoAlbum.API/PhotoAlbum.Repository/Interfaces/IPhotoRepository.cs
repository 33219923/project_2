using PhotoAlbum.Repository.Interfaces.Base;
using System.Collections.Generic;
using PhotoDto = PhotoAlbum.Shared.Models.Photo;
using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<PhotoDto, PhotoModel>
    {
        void RelinkPhotos(Shared.Models.PhotoRelink relink);
        List<PhotoDto> ListAll(string searchString);
    }
}
