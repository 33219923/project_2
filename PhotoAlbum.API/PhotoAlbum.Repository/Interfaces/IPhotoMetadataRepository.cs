using PhotoAlbum.Repository.Interfaces.Base;

using PhotoMetadataDto = PhotoAlbum.Shared.Models.PhotoMetadata;
using PhotoMetadataModel = PhotoAlbum.Data.Models.PhotoMetadata;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface IPhotoMetadataRepository : ISearchableRepository<PhotoMetadataDto, PhotoMetadataModel>
    {
    }
}
