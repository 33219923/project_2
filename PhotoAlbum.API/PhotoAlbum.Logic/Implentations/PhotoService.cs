using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;

using PhotoDto = PhotoAlbum.Shared.Models.Photo;
using PhotoModel = PhotoAlbum.Data.Models.Photo;

namespace PhotoAlbum.Logic.Implentations
{
    public class PhotoService : BaseService<PhotoDto, PhotoModel>, IPhotoService
    {
        public PhotoService(ILogger logger, IBaseRepository<PhotoDto, PhotoModel> repository) : base(logger, repository)
        {
        }
    }
}
