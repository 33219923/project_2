using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;

using AlbumDto = PhotoAlbum.Shared.Models.Album;
using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Logic.Implentations
{
    public class AlbumService : BaseService<AlbumDto, AlbumModel>, IAlbumService
    {
        public AlbumService(ILogger logger, IBaseRepository<AlbumDto, AlbumModel> repository) : base(logger, repository)
        {
        }
    }
}
