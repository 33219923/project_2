using PhotoAlbum.Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using SharedPhotoDto = PhotoAlbum.Shared.Models.SharedPhoto;
using SharedPhotoModel = PhotoAlbum.Data.Models.SharedPhoto;

namespace PhotoAlbum.Repository.Interfaces
{
    public interface ISharedPhotoRepository : IBaseRepository<SharedPhotoDto, SharedPhotoModel>
    {
        List<Shared.Models.Photo> ListAllShared();
        List<Shared.Models.UserReference> ListAvailableUsers(Guid photoId);
        List<Shared.Models.UserReference> ListSharedUsers(Guid photoId);
        Shared.Models.PhotoMetadata UpsertMetadata(Shared.Models.PhotoMetadata metadata);
    }
}
