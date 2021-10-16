using PhotoAlbum.Data;
using PhotoAlbum.Repository.Implementations.Base;
using PhotoAlbum.Repository.Interfaces;
using System;
using System.Collections.Generic;

using PhotoMetadataModel = PhotoAlbum.Data.Models.PhotoMetadata;

namespace PhotoAlbum.Repository.Implementations
{
    public class PhotoMetadataRepository : SearchableRepository<PhotoMetadataModel>, IPhotoMetadataRepository<PhotoMetadataModel>
    {
        private readonly ApplicationDbContext _db;

        public PhotoMetadataRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public override List<PhotoMetadataModel> Search(string searchPhrase, Func<PhotoMetadataModel, bool> filter = null)
        {
            return base.Search(searchPhrase, filter);
        }
    }
}
