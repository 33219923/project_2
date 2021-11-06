using Microsoft.Extensions.Logging;
using PhotoAlbum.Logic.Interfaces;
using PhotoAlbum.Repository.Interfaces;
using PhotoAlbum.Repository.Interfaces.Base;
using PhotoAlbum.Shared.Models;
using AlbumDto = PhotoAlbum.Shared.Models.Album;
using AlbumModel = PhotoAlbum.Data.Models.Album;

namespace PhotoAlbum.Logic.Implentations
{
    public class AlbumService : BaseService<AlbumDto, AlbumModel>, IAlbumService
    {
        private readonly ISharedAlbumRepository _sharedAlbumRepository;
        public AlbumService(ILogger<AlbumService> logger, IAlbumRepository repository, ISharedAlbumRepository sharedAlbumRepository) : base(logger, repository)
        {
            _sharedAlbumRepository = sharedAlbumRepository;
        }

        public SharedAlbum ShareAlbum(SharedAlbum sharedAlbum)
        {
            return _sharedAlbumRepository.Add(sharedAlbum);
        }

        public void UnshareAlbum(SharedAlbum sharedAlbum)
        {
            _sharedAlbumRepository.Delete(x => x.AlbumId == sharedAlbum.AlbumId && x.UserId == sharedAlbum.UserId);
        }
    }
}
